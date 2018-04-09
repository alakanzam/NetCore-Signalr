namespace SignalrCore.Models
{
    public class JwtResponse
    {
        public string Code { get; set; }

        public int LifeTime { get; set; }

        public double Expiration { get; set; }
    }
}