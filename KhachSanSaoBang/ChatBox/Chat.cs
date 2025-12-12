using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSanSaoBang.ChatBox
{
    public partial class Chat : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly string apiKey;

        private const string HistoryFilePath = "Chat_history.txt";
        private const string KeyFilePath = "Gemini_key.txt";

        public Chat()
        {
            InitializeComponent();

            // --- 🎨 CẬP NHẬT GIAO DIỆN (FONT & MÀU) TẠI ĐÂY ---
            // 1. Đổi font chữ to và đẹp hơn (Segoe UI, cỡ 12)
            rtb_Chat.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            txt_Message.Font = new Font("Segoe UI", 12, FontStyle.Regular);

            // 2. Padding (tạo khoảng cách lề cho dễ nhìn - nếu RichTextBox hỗ trợ)
            rtb_Chat.BackColor = Color.WhiteSmoke; // Màu nền tổng thể xám siêu nhạt cho dịu mắt
            // ----------------------------------------------------

            apiKey = LoadApiKey();
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                MessageBox.Show("⚠️ Không tìm thấy API key! Kiểm tra lại file Gemini_key.txt.", "Lỗi API", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            btn_Send.Click += async (s, e) => await SendMessageAsync();
            btn_ViewHistory.Click += Btn_ViewHistory_Click;
            btn_ClearHistory.Click += Btn_ClearHistory_Click;

            InitializeAutoCompleteFromHistory();
        }

        private async Task SendMessageAsync()
        {
            string userMessage = txt_Message.Text.Trim();
            if (string.IsNullOrEmpty(userMessage)) return;

            // 🟢 THAY ĐỔI 1: Màu xanh nhạt cho người dùng (LightCyan)
            AppendChat("Bạn", userMessage, Color.LightSkyBlue, "👤");

            txt_Message.Clear();

            // Màu tạm thời khi AI đang suy nghĩ
            AppendChat("Trợ Lý", "Đang phản hồi...", Color.LightBlue, "🤖");

            string aiResponse = await GetGeminiResponse(userMessage);

            // 🔵 THAY ĐỔI 2: Màu xanh đậm hơn cho AI (LightSkyBlue)
            ReplaceLastMessage("Trợ Lý", aiResponse, Color.LightSkyBlue, "🤖");

            SaveChatHistory(userMessage, aiResponse);
            AddSuggestion(userMessage);
        }

        private async Task<string> GetGeminiResponse(string prompt)
        {
            try
            {
                string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";
                var requestData = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = "Bạn là trợ lý AI của khách sạn Sao Băng. Hãy trả lời ngắn gọn, thân thiện và đúng chủ đề du lịch, khách sạn.\nCâu hỏi: " + prompt }
                            }
                        }
                    }
                };

                string json = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                string result = await response.Content.ReadAsStringAsync();
                File.WriteAllText("last_response.json", result);

                JObject obj = JObject.Parse(result);
                string text = obj["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();

                if (string.IsNullOrWhiteSpace(text))
                    text = "[Không nhận được phản hồi từ Gemini.]";

                text = text.Replace("\r", "").Trim();

                return text;
            }
            catch (Exception ex)
            {
                return "Lỗi khi gọi Gemini: " + ex.Message;
            }
        }

        private void AppendChat(string senderName, string message, Color bgColor, string avatar)
        {
            // Định dạng tiêu đề (Tên người gửi)
            string header = $"{avatar} {senderName}:\n";

            rtb_Chat.SelectionStart = rtb_Chat.TextLength;
            rtb_Chat.SelectionLength = 0;

            // --- Tùy chỉnh Font cho tiêu đề (In đậm) ---
            rtb_Chat.SelectionFont = new Font(rtb_Chat.Font, FontStyle.Bold);
            rtb_Chat.SelectionColor = Color.Black;
            rtb_Chat.AppendText(header);

            // --- Tùy chỉnh màu nền và nội dung tin nhắn ---
            rtb_Chat.SelectionStart = rtb_Chat.TextLength;
            rtb_Chat.SelectionLength = 0; // Reset độ dài chọn
            rtb_Chat.SelectionFont = new Font(rtb_Chat.Font, FontStyle.Regular); // Trả về font thường
            rtb_Chat.SelectionBackColor = bgColor; // Màu nền xanh

            // Thêm khoảng trắng để nhìn khung màu rộng hơn một chút
            rtb_Chat.AppendText(" " + message + " \n\n");

            // Reset lại màu nền về mặc định
            rtb_Chat.SelectionBackColor = rtb_Chat.BackColor;
            rtb_Chat.ScrollToCaret();
        }

        private void ReplaceLastMessage(string senderName, string newMessage, Color bgColor, string avatar)
        {
            string pattern = $"{avatar} {senderName}:\n Đang phản hồi... \n\n";

            // Tìm chuỗi "Đang phản hồi..." cũ (Lưu ý phải khớp chính xác với AppendChat)
            // Vì ở AppendChat mình đã thêm \n và khoảng trắng, nên tìm kiếm ở đây hơi khó chính xác tuyệt đối.
            // Cách đơn giản nhất để tránh lỗi tìm kiếm là xóa dòng cuối và viết lại.

            // Tìm vị trí của header cuối cùng
            int index = rtb_Chat.Text.LastIndexOf($"{avatar} {senderName}:");

            if (index >= 0)
            {
                // Chọn từ vị trí header đó đến hết văn bản
                rtb_Chat.Select(index, rtb_Chat.TextLength - index);

                // Xóa nội dung cũ (Đang phản hồi...)
                rtb_Chat.SelectedText = "";

                // Ghi lại nội dung mới với màu chuẩn
                // Gọi lại logic tô màu như AppendChat nhưng không cần xuống dòng quá nhiều
                string header = $"{avatar} {senderName}:\n";

                // In đậm tên
                rtb_Chat.SelectionFont = new Font(rtb_Chat.Font, FontStyle.Bold);
                rtb_Chat.AppendText(header);

                // Nội dung tin nhắn AI
                rtb_Chat.SelectionFont = new Font(rtb_Chat.Font, FontStyle.Regular);
                rtb_Chat.SelectionBackColor = bgColor; // Màu xanh đậm
                rtb_Chat.AppendText(" " + newMessage + " \n\n");

                rtb_Chat.SelectionBackColor = rtb_Chat.BackColor;
                rtb_Chat.ScrollToCaret();
            }
            else
            {
                // Nếu không tìm thấy (trường hợp hiếm), cứ append mới
                AppendChat(senderName, newMessage, bgColor, avatar);
            }
        }

        private void SaveChatHistory(string userMsg, string aiMsg)
        {
            string log = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]\nBạn: {userMsg}\nTrợ Lý: {aiMsg}\n\n";
            File.AppendAllText(HistoryFilePath, log);
        }

        private void Btn_ViewHistory_Click(object sender, EventArgs e)
        {
            if (!File.Exists(HistoryFilePath))
            {
                MessageBox.Show("Chưa có lịch sử chat.", "Lịch sử", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = HistoryFilePath,
                UseShellExecute = true
            });
        }

        private void Btn_ClearHistory_Click(object sender, EventArgs e)
        {
            if (File.Exists(HistoryFilePath))
            {
                File.Delete(HistoryFilePath);
                MessageBox.Show("Đã xóa lịch sử chat.", "Xóa lịch sử", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void InitializeAutoCompleteFromHistory()
        {
            txt_Message.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt_Message.AutoCompleteSource = AutoCompleteSource.CustomSource;

            var coll = new AutoCompleteStringCollection();
            coll.AddRange(new[]
            {
                "Xin chào", "Phòng đơn giá bao nhiêu?", "Gợi ý du lịch ở Đà Lạt",
                "Khách sạn có giảm giá không?", "Đặt phòng như thế nào?"
            });

            if (File.Exists(HistoryFilePath))
            {
                foreach (var line in File.ReadAllLines(HistoryFilePath))
                {
                    if (line.StartsWith("Bạn:"))
                    {
                        string content = line.Substring(4).Trim();
                        if (!coll.Contains(content))
                            coll.Add(content);
                    }
                }
            }

            txt_Message.AutoCompleteCustomSource = coll;
        }

        private void AddSuggestion(string suggestion)
        {
            var coll = txt_Message.AutoCompleteCustomSource;
            if (!coll.Contains(suggestion))
                coll.Add(suggestion);
        }

        private string LoadApiKey()
        {
            string keyPath = Path.Combine(Application.StartupPath, "Gemini_key.txt");
            return File.Exists(keyPath) ? File.ReadAllText(keyPath).Trim() : "";
        }
    }
}