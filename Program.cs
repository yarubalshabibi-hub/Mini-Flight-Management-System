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

        static int currentRow = 10;
        static char currentSeat = 'A';
        static void Main(string[] args)
        {
            ///passengerNames
            passengerNames.Add("Ahmed");
            passengerNames.Add("Ali");
            passengerNames.Add("Mohammed");
            passengerNames.Add("Salim");
            passengerNames.Add("Khalid");

            ///ticketNumbers
            ticketNumbers.Add("TKT1");
            ticketNumbers.Add("TKT2");
            ticketNumbers.Add("TKT3");
            ticketNumbers.Add("TKT4");
            ticketNumbers.Add("TKT5");

            /// availableDates
            availableDates.Add("12-5-2026");
            availableDates.Add("15-6-2027");
            availableDates.Add("20-7-2028");
            availableDates.Add("05-8-2029");

            int choice = -1;
            while (choice != 0)
            {
                PrintMenu();
                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Please enter a valid number: ");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        RegisterPassenger();
                        break;

                    case 2:
                        ViewAllPassengers();
                        break;

                    case 3:
                        BookFlight();
                        break;

                    case 4:ViewBookingDetails();
                        break;

                    case 5:;
                        break;

                    case 6:;
                        break;

                    case 7:;
                        break;

                    case 8:;
                        break;

                    case 9:;
                        break;

                    case 10:;
                        break;

                    case 0: Console.WriteLine("Goodbye Safe travels ");
                        break;

                    default: Console.WriteLine("Invalid option Please choose 0-10");
                        break;

                }
                Console.WriteLine();
            }
        }

        static void PrintMenu()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("  SKY WINGS FLIGHT MANAGEMENT SYSTEM   ");
            Console.WriteLine("========================================");
            Console.WriteLine("1.  Register New Passenger");
            Console.WriteLine("2.  View All Passengers");
            Console.WriteLine("3.  Book a Flight Ticket");
            Console.WriteLine("4.  View Booking Details");
            Console.WriteLine("5.  Update a Booking");
            Console.WriteLine("6.  Cancel a Ticket");
            Console.WriteLine("7.  Passenger Check in");
            Console.WriteLine("8.  Board Passengers Boarding Stack");
            Console.WriteLine("9.  Generate Flight Manifest");
            Console.WriteLine("10. Manage Waitlist & Seat Assignment");
            Console.WriteLine("0. Exit");
            Console.WriteLine("========================================");
        }
       
        /// RegisterPassenger
           static void RegisterPassenger()
        {
            Console.WriteLine("Register New Passenger: ");
            Console.Write("Enter passenger full name: ");
            string name = Console.ReadLine().Trim();

          
            if (name == "")
            {
                Console.WriteLine("Error: Name cannot be empty.");
                return;
            }

          
            for (int i = 0; i < passengerNames.Count; i++)
            {
                if (passengerNames[i].ToLower() == name.ToLower())
                {
                    Console.WriteLine("Error: Passenger already registered.");
                    return;
                }
            }

            int nextNum = passengerNames.Count + 1;
            string ticketId = "TKT" + nextNum.ToString("D3");

            passengerNames.Add(name);
            ticketNumbers.Add(ticketId);

            Console.WriteLine("Passenger registered successfully!");
            Console.WriteLine("Name: " + name + "  Ticket ID: " + ticketId);
        }
       
        /// ViewAllPassengers
                static void ViewAllPassengers()
        {
            Console.WriteLine("All Passengers: ");

            if (passengerNames.Count == 0)
            {
                Console.WriteLine("No passengers registered yet: ");
                return;
            }

            Console.WriteLine("No.Passenger Name:    Ticket ID:  Status:");
          

        
            for (int i = 0; i < passengerNames.Count; i++)
            {
                string status = "Active";
                if (cancelledTickets.Contains(ticketNumbers[i]))
                    status = "CANCELLED";

                Console.WriteLine((i + 1).ToString().PadRight(3) 
                    + passengerNames[i].PadRight(24) 
                    + ticketNumbers[i].PadRight(9) 
                    + status);
            }

            Console.WriteLine("Total passengers: " + passengerNames.Count);
        }
        static void BookFlight()
        {
            Console.WriteLine("Book a Flight Ticket: ");
            Console.Write("Enter ticket ID: ");
            string ticketId = Console.ReadLine().Trim().ToUpper();

          
            if (!ticketNumbers.Contains(ticketId))
            {
                Console.WriteLine("Error: Ticket ID not found");
                return;
            }

          
            if (cancelledTickets.Contains(ticketId))
            {
                Console.WriteLine("Error: This ticket has been cancelled");
                return;
            }

           
            if (bookingRecord.ContainsKey(ticketId))
            {
                Console.WriteLine("Error: Ticket already has a booking Use option 5 to update: ");
                return;
            }

           
            Console.WriteLine("Available Flights: ");
            for (int i = 0; i < flightNumbers.Length; i++)
                Console.WriteLine(i + ". " + flightNumbers[i]);

            Console.Write("Select flight enter index: ");
            if (!int.TryParse(Console.ReadLine(), out int flightIdx)
                || flightIdx < 0 || flightIdx >= flightNumbers.Length)
            {
                Console.WriteLine("Error: Invalid flight selection.");
                return;
            }

          
            Console.WriteLine("Available Dates :");
            for (int i = 0; i < availableDates.Count; i++)
                Console.WriteLine(i + ". " + availableDates[i]);

            Console.Write("Select date enter index: ");
            if (!int.TryParse(Console.ReadLine(), out int dateIdx)
                || dateIdx < 0 || dateIdx >= availableDates.Count)
            {
                Console.WriteLine("Error: Invalid date selection");
                return;
            }

            bookingRecord[ticketId] = flightNumbers[flightIdx] + "|" + availableDates[dateIdx];

            int idx = ticketNumbers.IndexOf(ticketId);
            Console.WriteLine("Booking confirmed: ");
            Console.WriteLine("Passenger: " + passengerNames[idx]);
            Console.WriteLine("Ticket ID: " + ticketId);
            Console.WriteLine("Flight: " + flightNumbers[flightIdx]);
            Console.WriteLine("Date: " + availableDates[dateIdx]);
        }
        static void ViewBookingDetails()
        {
            Console.WriteLine("View Booking Details: ");
            Console.Write("Enter ticket ID: ");
            string ticketId = Console.ReadLine().Trim().ToUpper();

            if (!ticketNumbers.Contains(ticketId))
            {
                Console.WriteLine("Error: Ticket ID not found");
                return;
            }

            int idx = ticketNumbers.IndexOf(ticketId);
            string passengerName = passengerNames[idx];

            if (cancelledTickets.Contains(ticketId))
            {
                Console.WriteLine("This ticket has been cancelled: ");
                return;
            }

                       if (!bookingRecord.ContainsKey(ticketId))
            {
                Console.WriteLine("No booking found for this ticket.");
                return;
            }

               string[] parts = bookingRecord[ticketId].Split();

            Console.WriteLine("Booking Summary: ");
            Console.WriteLine("Passenger: " + passengerName);
            Console.WriteLine("Ticket ID: " + ticketId);
            Console.WriteLine("Flight: " + parts[0]);
            Console.WriteLine("Date: " + parts[1]);
        }

    }
}


