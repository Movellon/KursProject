using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Markup;
using System.Linq.Expressions;
using System.Windows;
using KursShop.Pages;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows.Media;
using System.Diagnostics;
using System.Xml.Linq;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace KursShop
{
    internal class AppLog
    {
        public void CreateReport()
        {
            var document = new Document(PageSize.A4, 20, 20, 30, 20);

            string ttf = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALNBI.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

            var BacketList = App.Context.Products_Client.ToList();


            using (var writer = PdfWriter.GetInstance(document, new FileStream("ReportProductClient.pdf", FileMode.Create)))
            {
                document.Open();
                document.NewPage();
                document.Add(new Paragraph("Чек", font));

                int Summ = 0;

                foreach (var Element in BacketList)
                {
                    document.Add(new Paragraph($"{Element.nameProduct}", font));
                    document.Add(new Paragraph($"Количество: {Element.quantity}", font));

                    var Cost = App.Context.Product.FirstOrDefault(p => p.id_product == Element.id_product);

                    document.Add(new Paragraph($"Цена: {Cost.cost}x{Element.quantity} = {Cost.cost * Element.quantity}", font));

                    document.Add(new Paragraph($"-----------------------------------------------------------", font));

                    Summ += Convert.ToInt32(Cost.cost * Element.quantity);
                }

                document.Add(new Paragraph($"Сумма: {Summ}", font));

                document.Close();
                writer.Close();
            }

            Process.Start("ReportProductClient.pdf");
        }

        public string UpdateLvLPassword(String TextPassword)
        {
            int LvL = 0;

            string Password = TextPassword;

            if (TextPassword.Length >= 7)
                LvL++;

            string[] RegexExpressions = { "[a-z]", "[A-Z]", "[а-я]", "[А-Я]", "[0-9]", @"\W"};

            foreach (string Expressions in RegexExpressions)
            {
                Regex regex = new Regex(Expressions);

                MatchCollection matches = regex.Matches(Password);
                if (matches.Count > 0)
                {
                    LvL++;
                }
            }

            Regex regexSpace = new Regex(@"\s");
            MatchCollection matchesSpace = regexSpace.Matches(Password);

            if (matchesSpace.Count > 0)
            {
                login.FalseRegister = true;
                return "Использование пробелов запрещено!";
            }
            if (LvL == 0)
            {
                login.FalseRegister = true;
                return "";
            }
            else if (LvL <= 3)
            {
                login.FalseRegister = true;
                return "Слабый пароль";
            }
            else if (LvL <= 4)
            {
                login.FalseRegister = false;
                return "Хороший пароль";
            }
            else if (LvL == 5)
            {
                login.FalseRegister = false;
                return "Надежный пароль";
            }

            return "";
        }

        public string CheckLogin(string LoginText)
        {
            var FindUser = App.Context.Clients.FirstOrDefault(p => p.login == LoginText);

            Regex regexSpace = new Regex(@"\s");
            MatchCollection matchesSpace = regexSpace.Matches(LoginText);

            if (matchesSpace.Count > 0)
            {
                login.FalseRegister = true;
                return "Использование пробелов запрещено!";
            }
            if (FindUser == null)
            {
                login.FalseRegister = false;
                return "";
            }
            else
            {
                login.FalseRegister = true;
                return "Логин занят другим пользователем";
            }
        }
    }
}
