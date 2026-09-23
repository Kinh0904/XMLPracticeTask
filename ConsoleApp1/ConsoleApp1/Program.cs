using System;
using System.IO;
using System.Text;
using System.Xml;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        string filePath = "SachInventory.xml";

        TaoFileXmlBanDau(filePath);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePath);

        Console.WriteLine("--- DỮ LIỆU BAN ĐẦU ---");
        HienThiDanhSachText(xmlDoc);

        Console.WriteLine("\n--- QUÁ TRÌNH XỬ LÝ DỮ LIỆU ---");

        // 1. Tìm sách 'CongNghe' và cộng 10 vào SoLuongTon
        XmlNodeList dsSachCongNghe = xmlDoc.SelectNodes("//Sach[@TheLoai='CongNghe']");
        if (dsSachCongNghe != null)
        {
            foreach (XmlNode sach in dsSachCongNghe)
            {
                string ma = sach.Attributes?["Ma"]?.Value ?? "";
                string tieuDe = sach.SelectSingleNode("TieuDe")?.InnerText ?? "";
                XmlNode nodeSoLuong = sach.SelectSingleNode("SoLuongTon");

                if (nodeSoLuong != null && int.TryParse(nodeSoLuong.InnerText, out int slCu))
                {
                    int slMoi = slCu + 10;
                    nodeSoLuong.InnerText = slMoi.ToString();
                    Console.WriteLine($"[Cập nhật] Mã: {ma} - Tiêu đề: {tieuDe} - Số lượng: {slCu} -> {slMoi}");
                }
            }
        }

        // 2. Tìm sách có SoLuongTon = 0 và xóa bỏ
        XmlNodeList dsSachHetHang = xmlDoc.SelectNodes("//Sach[SoLuongTon='0']");
        if (dsSachHetHang != null)
        {
            foreach (XmlNode sach in dsSachHetHang)
            {
                string ma = sach.Attributes?["Ma"]?.Value ?? "";
                string tieuDe = sach.SelectSingleNode("TieuDe")?.InnerText ?? "";

                Console.WriteLine($"[Xóa bỏ]   Mã: {ma} - Tiêu đề: {tieuDe} - Lý do: SoLuongTon = 0");
                sach.ParentNode?.RemoveChild(sach);
            }
        }

        Console.WriteLine("\n--- DỮ LIỆU SAU KHI CẬP NHẬT ---");
        HienThiDanhSachText(xmlDoc);

        // Lưu kết quả
        string updatedFilePath = "SachInventory_Updated.xml";
        xmlDoc.Save(updatedFilePath);
        Console.WriteLine($"\nĐã lưu kết quả vào tệp: {updatedFilePath}");
    }

    /// <summary>
    /// Xuất danh sách dạng văn bản dòng đơn giản
    /// </summary>
    static void HienThiDanhSachText(XmlDocument doc)
    {
        XmlNodeList dsSach = doc.SelectNodes("//Sach");
        if (dsSach == null || dsSach.Count == 0)
        {
            Console.WriteLine("Kho hàng trống.");
            return;
        }

        foreach (XmlNode sach in dsSach)
        {
            string ma = sach.Attributes?["Ma"]?.Value ?? "";
            string theLoai = sach.Attributes?["TheLoai"]?.Value ?? "";
            string tieuDe = sach.SelectSingleNode("TieuDe")?.InnerText ?? "";
            string soLuong = sach.SelectSingleNode("SoLuongTon")?.InnerText ?? "0";

            Console.WriteLine($"Mã sách: {ma} | Thể loại: {theLoai} | Tên sách: {tieuDe} | Số lượng tồn: {soLuong}");
        }
    }

    static void TaoFileXmlBanDau(string path)
    {
        string content = @"<?xml version=""1.0"" encoding=""utf-8""?>
<ThuVien>
    <Sach Ma=""S01"" TheLoai=""CongNghe"">
        <TieuDe>Lập trình C# nâng cao với .NET 8</TieuDe>
        <SoLuongTon>15</SoLuongTon>
    </Sach>
    <Sach Ma=""S02"" TheLoai=""VanHoc"">
        <TieuDe>Lão Hạc và cuộc đời</TieuDe>
        <SoLuongTon>0</SoLuongTon>
    </Sach>
    <Sach Ma=""S03"" TheLoai=""CongNghe"">
        <TieuDe>Thiết kế cơ sở dữ liệu XML</TieuDe>
        <SoLuongTon>5</SoLuongTon>
    </Sach>
</ThuVien>";
        File.WriteAllText(path, content, Encoding.UTF8);
    }
}