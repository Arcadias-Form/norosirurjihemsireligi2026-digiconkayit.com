using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace VeritabaniIslemMerkezi
{
    public class MailGonderimIslemleri
    {
        StringBuilder OutlookContent;
        string textCenter = "text-align: center;";

        public void KayitBilgilendirme(string OdemeID, string DilID)
        {
            KayitBilgilendirme(new OdemeTablosuIslemler().KayitBilgisi(OdemeID, DilID).Veriler, DilID);
        }


        public void KayitBilgilendirme(OdemeTablosuModel YeniKayit, string DilID = "tr")
        {
            using (MimeMessage mm = new MimeMessage())
            {
                mm.From.Add(new MailboxAddress("Türk Nörosirurji Dernegi 39. Bilimsel Kongresi", "tnd2026@digiconkayit.com"));
                mm.To.Add(new MailboxAddress(YeniKayit.KatilimciBilgisi.AdSoyad, YeniKayit.KatilimciBilgisi.ePosta));
             //   mm.Bcc.Add(new MailboxAddress("Türk Nörosirurji Dernegi 39. Bilimsel Kongresi", "tnd2026@honestglobal.com"));

                switch (DilID)
                {
                    default:
                    case "tr":
                        mm.Subject = "Türk Nörosirurji Dernegi 39. Bilimsel Kongresi Kayýt Bilgilendirme";

                        string tableStyle = "width:100%; border-collapse:collapse; font-family:Arial, sans-serif;";
                        string headerStyle = "background-color:#99ccff; color:#000000; text-align:center; font-weight:bold;";
                        string subHeaderStyle = "background-color:#cce6ff; color:#000000; text-align:center; font-weight:bold;";
                        string rowStyle = "background-color:#ffffff; color:#000000;";
                        string cellStyle = "padding:8px; border:1px solid #99ccff;";

                        OutlookContent = new StringBuilder()
                            .Append($"<p>Sayýn {YeniKayit.KatilimciBilgisi.AdSoyad},</p>")
                            .Append($"<div><p>09 - 12 Nisan 2026 tarihlerinde gerçekleþecek olan Türk Nörosirurji Dernegi 39. Bilimsel Kongresine kayýt yaptýrdýðýnýz için teþekkür ederiz.</p>{(YeniKayit.OdemeTipiID.Equals(1) ? $"<p>Ön kaydýnýz, ödeme sonrasý dekontunuzu ilettiðinizde onaylanacaktýr.</p><p><u><b>Ödeme tutarý, ödeme yaptýðýnýz tarihteki dönem ücretine göre yapýlmalýdýr.</b></u> Aksi taktirde ön kaydýnýz onaylanmayacaktýr.</p><p>Ödemeyi, <u><b>sipariþ numarasý</b></u>'ný dekontunuzda belirterek yapmayý unutmayýnýz. Dekontunuzu <a href=\"mailto:tnd2026@honestglobal.com\">tnd2026@honestglobal.com</a> adresine iletmeyi unutmayýnýz.</p><p></p>" : string.Empty)}<p>Katýlým bilgileriniz aþaðýdadýr:</p></div>")
                            .Append($"<table style=\"{tableStyle}\">")
                            .Append($"<tr><td colspan=\"2\" style=\"{headerStyle}\">Kiþisel Bilgiler</td></tr>")
                            .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle} width:30%;\">Kimlik No</td><td style=\"{cellStyle}\">{YeniKayit.KatilimciBilgisi.KimlikNo}</td></tr>")
                            .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Cinsiyet</td><td style=\"{cellStyle}\">{YeniKayit.KatilimciBilgisi.Cinsiyet}</td></tr>")
                            .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">e-Posta</td><td style=\"{cellStyle}\">{YeniKayit.KatilimciBilgisi.ePosta}</td></tr>")
                            .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Cep Telefonu</td><td style=\"{cellStyle}\">{YeniKayit.KatilimciBilgisi.CepTelefonu}</td></tr>")
                            .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Kurum</td><td style=\"{cellStyle}\">{YeniKayit.KatilimciBilgisi.Kurum}</td></tr>")
                            .Append($"<tr><td colspan=\"2\" style=\"{cellStyle}\">&nbsp;</td></tr>")
                            .Append($"<tr><td colspan=\"2\" style=\"{subHeaderStyle}\">Hizmetler</td></tr>")
                            .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Katýlýmcý Tipi</td><td style=\"{cellStyle}\">{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.KatilimciTipiBilgisi.KatilimciTipiDilBilgisi.First().KatilimciTipi}</td></tr>")
                            .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Konaklama</td><td style=\"{cellStyle}\">{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi.OtelBilgisi.Otel} / {YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi.OdaTipiDilBilgisi.First().OdaTipi}</td></tr>")
                            .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Giriþ Tarihi</td><td style=\"{cellStyle}\">{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.GirisTarihi:dd.MM.yyyy}</td></tr>")
                            .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Çýkýþ Tarihi</td><td style=\"{cellStyle}\">{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.CikisTarihi:dd.MM.yyyy}</td></tr>");

                        if (YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi.RefakatciDurum)
                        {
                            OutlookContent
                                .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Refakatçi</td><td style=\"{cellStyle}\">{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.Refakatci}</td></tr>");
                        }

                        OutlookContent
                            .Append($"<tr><td colspan=\"2\" style=\"{cellStyle}\">&nbsp;</td></tr>")
                            .Append($"<tr><td colspan=\"2\" style=\"{subHeaderStyle}\">Ödeme ve Fatura Bilgileri</td></tr>")
                            .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Fatura Tipi</td><td style=\"{cellStyle}\">{YeniKayit.KatilimciBilgisi.FaturaTipi}</td></tr>")
                            .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Fatura Ünvaný</td><td style=\"{cellStyle}\">{YeniKayit.KatilimciBilgisi.FaturaUnvan}</td></tr>")
                            .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Fatura Adresi</td><td style=\"{cellStyle}\">{YeniKayit.KatilimciBilgisi.FaturaAdres}</td></tr>")
                            .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Vergi Dairesi</td><td style=\"{cellStyle}\">{YeniKayit.KatilimciBilgisi.VergiDairesi}</td></tr>")
                            .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Vergi No</td><td style=\"{cellStyle}\">{YeniKayit.KatilimciBilgisi.VergiNo}</td></tr>")
                            .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Ödeme Tipi</td><td style=\"{cellStyle}\">{YeniKayit.OdemeTipiBilgisi.OdemeTipiDilBilgisi.First().OdemeTipi}</td></tr>")
                            .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Sipariþ Numarasý</td><td style=\"{cellStyle}\">{YeniKayit.OdemeID}</td></tr>")
                            .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Toplam</td><td style=\"{cellStyle}\">{YeniKayit.DovizUcret}</td></tr>");

                        if (YeniKayit.OdemeTipiID.Equals(1))
                        {
                            OutlookContent
                                .Append($"<tr><td colspan=\"2\" style=\"{cellStyle}\">&nbsp;</td></tr>")
                                .Append($"<tr><td colspan=\"2\" style=\"{subHeaderStyle}\">Banka Havalesi Bilgisi</td></tr>")
                                .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Banka</td><td style=\"{cellStyle}\"> AKBANK </td></tr>")
                                .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Hesap Adý</td><td style=\"{cellStyle}\">  AKTÝV TURÝZM SEYEHAT VE KARGO TAÞIMACILIÐI </td></tr>")
                                .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Þube</td><td style=\"{cellStyle}\">  HASANPAÞA ÞUBE - 0235 </td></tr>")
                                .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">TL IBAN</td><td style=\"{cellStyle}\">  TR36 0004 6002 3588 8000 1624 39 </td></tr>")
                                .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">EURO IBAN</td><td style=\"{cellStyle}\">  TR80 0004 6002 3503 6000 1296 99 </td></tr>")
                                .Append($"<tr style=\"{rowStyle}\"><td style=\"{cellStyle}\">Açýklama</td><td style=\"{cellStyle}\">  ISTKONGRETND_2026</td></tr>");
                        }

                        OutlookContent.Append("</table>");
                        break;

                    case "en":
                        mm.Subject = "ARC 2024 Regsitration Information";

                        OutlookContent = new StringBuilder()
                            .Append($"<p>Dear {YeniKayit.KatilimciBilgisi.AdSoyad},</p>")
                            .Append($"<div><p>Thank you for registering for the ARC Congress.</p>{(YeniKayit.OdemeTipiID.Equals(1) ? $"<p>Your pre-registration will be confirmed when you submit your receipt after payment.</p><p><u><b>The payment amount must be made according to the term fee on the date you make the payment.</b></u> Otherwise, your pre-registration will not be approved. p><p>Do not forget to make the payment by stating the <u><b>order number</b></u> on your receipt. Do not forget to send your receipt to <a href=\"mailto:...\">...@.........</a>.</p><p>&nbsp;</p>" : string.Empty)}<p>Your participation information is below:</p></div>")
                            .Append("<table style=\"width:100%;\" border=\"1\">")
                            .Append($"<tr><td colspan=\"2\" style=\"{textCenter}\"><b>Personal Information</b></td></tr>")
                            .Append($"<tr><td style=\"width:30%;\">Passaport No</td><td>{YeniKayit.KatilimciBilgisi.KimlikNo}</td></tr>")
                            .Append($"<tr><td>Gender</td><td>{YeniKayit.KatilimciBilgisi.Cinsiyet}</td></tr>")
                            .Append($"<tr><td>e-Mail</td><td>{YeniKayit.KatilimciBilgisi.ePosta}</td></tr>")
                            .Append($"<tr><td>Mobile Phone</td><td>{YeniKayit.KatilimciBilgisi.CepTelefonu}</td></tr>")
                            .Append($"<tr><td>Institution</td><td>{YeniKayit.KatilimciBilgisi.Kurum}</td></tr>")
                            .Append($"<tr><td colspan=\"2\">&nbsp;</td></tr>")
                            .Append($"<tr><td colspan=\"2\" style=\"{textCenter}\"><b>Services</b></td></tr>")
                            .Append($"<tr><td>Registration Type</td><td>{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.KatilimciTipiBilgisi.KatilimciTipiDilBilgisi.First().KatilimciTipi}</td></tr>")
                            .Append($"<tr><td>Accommoadtion</td><td>{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi.OtelBilgisi.Otel} / {YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi.OdaTipiDilBilgisi.First().OdaTipi}</td></tr>")
                            .Append($"<tr><td>Check-In Date</td><td>{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.GirisTarihi:dd.MM.yyyy}</td></tr>")
                            .Append($"<tr><td>Check-Out Date</td><td>{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.CikisTarihi:dd.MM.yyyy}</td></tr>");

                        if (YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.KatilimciTipiOdaTipiBilgisi.OdaTipiBilgisi.RefakatciDurum)
                        {
                            OutlookContent
                                .Append($"<tr><td>Accommpaying' Person</td><td>{YeniKayit.KatilimciBilgisi.KonaklamaBilgisi.Refakatci}</td></tr>");
                        }

                        OutlookContent
                            .Append($"<tr><td>Transfer</td><td>{YeniKayit.KatilimciBilgisi.TransferBilgisi.TransferTipiBilgisi.TransferTipiDilBilgisi.First().TransferTipi}</td></tr>")
                            .Append($"<tr><td>Course(s)</td><td>{string.Join(", ", (YeniKayit.KatilimciBilgisi.KatilimciKursBilgisi is null ? new List<string>() : YeniKayit.KatilimciBilgisi.KatilimciKursBilgisi.Select(x => x.KursTipiBilgisi.KursTipiDilBilgisi.First().KursTipi).ToList()))}</td></tr>")
                            .Append($"<tr><td>Event(s)</td><td>{string.Join(", ", (YeniKayit.KatilimciBilgisi.KatilimciEtkinlikBilgisi is null ? new List<string>() : YeniKayit.KatilimciBilgisi.KatilimciEtkinlikBilgisi.Select(x => x.EtkinlikBilgisi.EtkinlikDilBilgisi.First().Etkinlik).ToList()))}</td></tr>")
                            .Append("<tr><td colspan=\"2\">&nbsp;</td></tr>")
                            .Append($"<tr><td colspan=\"2\" style=\"{textCenter}\"><b>Payment & Invoice Informations</b></td></tr>")
                            .Append($"<tr><td>Invoice Type</td><td>{YeniKayit.KatilimciBilgisi.FaturaTipi}</td></tr>")
                            .Append($"<tr><td>Invoice Title</td><td>{YeniKayit.KatilimciBilgisi.FaturaUnvan}</td></tr>")
                            .Append($"<tr><td>Invoice Address</td><td>{YeniKayit.KatilimciBilgisi.FaturaAdres}</td></tr>")
                            .Append($"<tr><td>Tax Office</td><td>{YeniKayit.KatilimciBilgisi.VergiDairesi}</td></tr>")
                            .Append($"<tr><td>Tax No</td><td>{YeniKayit.KatilimciBilgisi.VergiNo}</td></tr>")
                            .Append($"<tr><td>Payment Type</td><td>{YeniKayit.OdemeTipiBilgisi.OdemeTipiDilBilgisi.First().OdemeTipi}</td></tr>")
                            .Append($"<tr><td>Order No</td><td>{YeniKayit.OdemeID}</td></tr>")
                            .Append($"<tr><td>Total</td><td>{YeniKayit.DovizUcret}</td></tr>");

                        if (YeniKayit.OdemeTipiID.Equals(1))
                        {
                            OutlookContent
                                .Append("<tr><td colspan=\"2\">&nbsp;</td></tr>")
                                .Append($"<tr><td colspan=\"2\" style=\"{textCenter}\"><b>Bank Transfer Information</b></td></tr>")
                                .Append($"<tr><td>Hesap Adý</td><td> *** </td></tr>")
                                .Append($"<tr><td>Banka</td><td> *** </td></tr>")
                                .Append($"<tr><td>Þube</td><td> *** </td></tr>")
                                .Append($"<tr><td>Hesap No</td><td> *** </td></tr>")
                                .Append($"<tr><td>IBAN</td><td> *** </td></tr>");
                        }

                        OutlookContent
                            .Append("</table>");
                        break;
                }



                mm.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    ContentTransferEncoding = ContentEncoding.Base64,
                    Text = OutlookContent.ToString()
                };

                using (ProtocolLogger logger = new ProtocolLogger(HttpContext.Current.Server.MapPath($"~/Dosyalar/MailLog/{YeniKayit.KatilimciBilgisi.ePosta}_{DateTime.Now:yyyy.MM.dd HH.mm.ss}.log")))
                {
                    using (SmtpClient client = new SmtpClient(logger))
                    {
                        try
                        {
                            client.Connect("mail.digiconkayit.com", 587, MailKit.Security.SecureSocketOptions.None);
                            client.AuthenticationMechanisms.Remove("DIGEST-MD5");
                            client.Authenticate("tnd2026@digiconkayit.com", "eaQ-g8fh");

                            client.Send(mm);
                            client.Disconnect(true);
                        }
                        catch (Exception ex)
                        {
                            byte[] exToArray = Encoding.UTF8.GetBytes(ex.Message);
                            logger.Stream.Write(exToArray, 0, exToArray.Length);
                            client.Disconnect(false);
                        }
                    }
                }
            }
        }
    }
}
