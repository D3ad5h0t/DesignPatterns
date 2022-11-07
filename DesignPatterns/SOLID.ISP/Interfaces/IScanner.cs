using SOLID.ISP.Models;

namespace SOLID.ISP.Interfaces;

public interface IScanner
{
    void Scan(Document document);
}