namespace RealTime_D3.Models
{
    public class Tbllog
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string Detail { get; set; } = "";
        public DateTime LogDate { get; set; }
    }
}
