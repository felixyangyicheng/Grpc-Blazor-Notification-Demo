namespace WASM_Reatime_D3.Models
{
    public class TbllogInfo
    {
        public string table { get; set; } = "";
        public string action { get; set; } = "";
        public Tbllog data { get; set; } = new();
    }
}