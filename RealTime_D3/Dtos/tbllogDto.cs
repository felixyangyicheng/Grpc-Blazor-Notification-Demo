namespace RealTime_D3.Dtos
{
    public class TbllogDto:BaseDto
    {
        public int Value { get; set; }
        public string Detail { get; set; } = "";
        public DateTime LogDate { get; set; }
    }
}
