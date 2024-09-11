namespace microTrading.dto
{
    public class RateInfoDto
    {
        public decimal close { get; set; }
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        //ctm is a timestamp
        public long ctm {  get; set; }
        //ctm date is a date in a string "Jan 10, 2014 3:04:00 PM"
        public string ctmString { get; set; } = "";
        //Volume in lot ex: if 10000 one lont contains 10000 units.
        public float vol { get; set; }


        public override string ToString()
        {
            return $"Close: {close}, Open: {open}, High: {high}, Low: {low}, " +
                   $"CTM (Timestamp): {ctm}, CTM Date: {ctmString}, Volume: {vol} lots";
        }
    }
}
