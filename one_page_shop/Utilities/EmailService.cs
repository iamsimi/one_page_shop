using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MailKit.Net.Smtp;
using MimeKit;
using one_page_shop.Models;

namespace one_page_shop.Utilities
{
    public class EmailService : IEmailService, ICreatePdfService
    {       
        public string CreatePdf(IList<Product> products)
        {
            string _path = Path.Combine(Environment.CurrentDirectory, "sales_sheet.pdf");

            FileStream file = File.Create(_path);
            Document document = new Document(PageSize.A4);
            document.SetMargins(5f, 5f, 10f, 5f);
            PdfWriter writer = PdfWriter.GetInstance(document, file);

            try
            {
                document.AddAuthor("One page shop");
                document.AddKeywords("Shopping");
                document.AddTitle("The items you bought");

                document.Open();

                Font heading = FontFactory.GetFont("arial", 13);
                Font body = FontFactory.GetFont("arial", 12);

                List<Phrase> phrases = new List<Phrase>();
                foreach (var item in products)
                {
                    Paragraph p1 = new Paragraph(item.Name + " " + item.Price, body);
                    phrases.Add(p1);
                }
                foreach (var item in phrases)
                {
                    document.Add(item);
                }
            }
            catch (DocumentException documentException)
            {
                throw documentException;
            }
            catch (IOException ioException)
            {
                throw ioException;
            }
            finally
            {
                document.Close();
                writer.Close();
                file.Close();
            }

            return _path;
        }

        public void SendEmail(string path, string sourceEmail, string targetEmail, string emailPasword)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("One Page shop'", sourceEmail));
            message.To.Add(new MailboxAddress("Items you bought", targetEmail));

            message.Subject = "Items you bought from our shop";
            var body = new TextPart("plain")
            {
                Text = "Thank you for shopping with us. We hope you time spent with us was worth it" +
                "and that we can see you some other time. Do recommend us to your friends and share with your family" 
            };
            var attachment = new MimePart("sales_sheet", "pdf")
            {
                Content = new MimeContent(File.OpenRead(path), ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName(path)
            };

            var multipart = new Multipart("mixed");
            multipart.Add(body);
            multipart.Add(attachment);

            message.Body = multipart;

            //configuring smtp.client 
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(targetEmail, emailPasword);
                client.Send(message);
                client.Disconnect(true);
            }
        }

    }
}
