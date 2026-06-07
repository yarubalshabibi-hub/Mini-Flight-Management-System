namespace Mini_Flight_Management_System
{
    internal class Program
    {
        static List<string> passengerNames = new List<string>();
        static List<string> ticketNumbers = new List<string>();
        static string[] flightNumbers = { "OA101", "OA102", "OA103", "OA104", "OA105", "OA106" };
        static List<string> availableDates = new List<string>();
        static Dictionary<string, string> bookingRecord = new Dictionary<string, string>();
        static Queue<string> checkedInQueue = new Queue<string>();
        static Stack<string> boardingStack = new Stack<string>();
        static List<string> cancelledTickets = new List<string>();
        static Dictionary<string, string> passengerSeatMap = new Dictionary<string, string>();
        static Queue<string> waitlistQueue = new Queue<string>();

        static void Main(string[] args)
        {
            
        }
    }
}
