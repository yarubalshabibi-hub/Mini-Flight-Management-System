using Microsoft.VisualBasic.FileIO;

namespace Mini_Flight_Management_System
{
    internal class Program
    {              // ── هذي المتغيرات المشتركة بين كل الـ functions ──────────────────────

        // قائمة بأسماء المسافرين
        static List<string> passengerNames = new List<string>();

        // قائمة بأرقام التذاكر - نفس الترتيب مع passengerNames
        static List<string> ticketNumbers = new List<string>();

        // مصفوفة أرقام الرحلات - ثابتة لا تتغير
        static string[] flightNumbers = { "OA101", "OA102", "OA103", "OA104", "OA105", "OA106" };

        // قائمة التواريخ المتاحة للحجز
        static List<string> availableDates = new List<string>();

        // Dictionary الحجوزات - المفتاح رقم التذكرة والقيمة رقم الرحلة|التاريخ
        static Dictionary<string, string> bookingRecord = new Dictionary<string, string>();

        // Queue المسافرين اللي سجلوا check-in - FIFO
        static Queue<string> checkedInQueue = new Queue<string>();

        // Stack المسافرين اللي يصعدون الطائرة - LIFO
        static Stack<string> boardingStack = new Stack<string>();

        // قائمة التذاكر الملغية
        static List<string> cancelledTickets = new List<string>();

        // Dictionary تعيين المقاعد - المفتاح اسم المسافر والقيمة رقم المقعد
        static Dictionary<string, string> passengerSeatMap = new Dictionary<string, string>();

        // Queue قائمة الانتظار للمسافرين الاحتياطيين
        static Queue<string> waitlistQueue = new Queue<string>();

        // متغيرات تتبع المقاعد - رقم الصف والحرف
        static int currentRow = 10;
        static char currentSeat = 'A';

        // ── نقطة بداية البرنامج ───────────────────────────────────────────────
        static void Main(string[] args)
        {
            // تحميل بيانات المسافرين الأولية
            passengerNames.Add("Ahmed");
            passengerNames.Add("Ali");
            passengerNames.Add("Mohammed");
            passengerNames.Add("Salim");
            passengerNames.Add("Khalid");

            // تحميل أرقام التذاكر - نفس الترتيب مع الأسماء
            ticketNumbers.Add("TKT-001");
            ticketNumbers.Add("TKT-002");
            ticketNumbers.Add("TKT-003");
            ticketNumbers.Add("TKT-004");
            ticketNumbers.Add("TKT-005");

            // تحميل التواريخ المتاحة
            availableDates.Add("12-05-2026");
            availableDates.Add("15-06-2027");
            availableDates.Add("20-07-2028");
            availableDates.Add("05-08-2029");

            int choice = -1;

            // حلقة البرنامج الرئيسية - تستمر حتى يختار المستخدم 0
            while (choice != 0)
            {
                PrintMenu();
                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();

                // int.TryParse تحاول تحول النص لرقم - لو فشلت ترجع false
                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue; // ارجع لأول الـ while بدون ما تكمل
                }

                // switch يوجه كل رقم للـ function الصحيحة

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

                    case 4:
                        ViewBookingDetails();
                        break;

                    case 5:
                        UpdateBooking();
                        break;

                    case 6:
                        CancelTicket();
                        break;

                    case 7:
                        PassengerCheckIn();
                        break;

                    case 8:
                        ;
                        break;

                    case 9:
                        ;
                        break;

                    case 10:
                        ;
                        break;

                    case 0:
                        Console.WriteLine("Goodbye Safe travels ");
                        break;

                    default:
                        Console.WriteLine("Invalid option Please choose 0-10");
                        break;

                }
                Console.WriteLine();// سطر فاضي بين كل عملية والثانية
            }
        }
        // طباعة القائمة الرئيسية على الشاشة
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

        /// تسجيل مسافر جديد
        static void RegisterPassenger()
        {
            Console.WriteLine("Register New Passenger: ");
            Console.Write("Enter passenger full name: ");

            ///يحذف المسافات الزيادة من البداية والنهاية
            string name = Console.ReadLine().Trim();

            // تحقق أن الاسم مو فاضي
            if (name == "")
            {
                Console.WriteLine("Error: Name cannot be empty.");
                return;///اخرج من الـ function فوراً
            }

            // تحقق من التكرار - for loop تمشي على كل الأسماء الموجودة
            for (int i = 0; i < passengerNames.Count; i++)
            {
                // .ToLower() تحول للحروف الصغيرة عشان المقارنة تكون صحيحة
                if (passengerNames[i].ToLower() == name.ToLower())
                {
                    Console.WriteLine("Error: Passenger already registered.");
                    return;
                }
            }

            // توليد رقم التذكرة التلقائي بناء على عدد المسافرين الحالي
            // مثال: لو عندك 5 مسافرين الرقم التالي = 6 → TKT-006
            int nextNum = passengerNames.Count + 1;
            string ticketId = "TKT" + nextNum.ToString("D3");

            // أضف للقائمتين بنفس الوقت عشان يبقوا بنفس الـ index
            passengerNames.Add(name);
            ticketNumbers.Add(ticketId);

            Console.WriteLine("Passenger registered successfully!");
            Console.WriteLine("Name: " + name + "  Ticket ID: " + ticketId);
        }


        //عرض كل المسافرين
        static void ViewAllPassengers()
        {
            Console.WriteLine("All Passengers: ");

            // تحقق أن القائمة مو فاضية
            if (passengerNames.Count == 0)
            {
                Console.WriteLine("No passengers registered yet: ");
                return;
            }

            Console.WriteLine("No.Passenger Name:    Ticket ID:  Status:");


            // for loop عشان نقدر نوصل للقائمتين بنفس الـ index
            for (int i = 0; i < passengerNames.Count; i++)
            {
                // افترض الحالة Active
                string status = "Active";

                // لو التذكرة في قائمة الملغية غير الحالة
                if (cancelledTickets.Contains(ticketNumbers[i]))
                    status = "CANCELLED";


                // .PadRight() تضيف مسافات لليمين عشان يكون محاذي
                Console.WriteLine((i + 1).ToString().PadRight(3)
                    + passengerNames[i].PadRight(24)
                    + ticketNumbers[i].PadRight(9)
                    + status);
            }

            Console.WriteLine("Total passengers: " + passengerNames.Count);
        }

        //حجز رحلة
        static void BookFlight()
        {
            Console.WriteLine("Book a Flight Ticket: ");
            Console.Write("Enter ticket ID: ");

            // .ToUpper() يحول للحروف الكبيرة عشان ما يفرق بين TKT-001 و tkt-001
            string ticketId = Console.ReadLine().Trim().ToUpper();


            // تحقق أن التذكرة موجودة في القائمة
            if (!ticketNumbers.Contains(ticketId))
            {
                Console.WriteLine("Error: Ticket ID not found");
                return;
            }

            // تحقق أن التذكرة ليست ملغية
            if (cancelledTickets.Contains(ticketId))
            {
                Console.WriteLine("Error: This ticket has been cancelled");
                return;
            }

            // تحقق أن ما في حجز موجود بالفعل
            if (bookingRecord.ContainsKey(ticketId))
            {
                Console.WriteLine("Error: Ticket already has a booking Use option 5 to update: ");
                return;
            }

            // اعرض الرحلات المتاحة
            Console.WriteLine("Available Flights: ");
            for (int i = 0; i < flightNumbers.Length; i++)
                Console.WriteLine(i + ". " + flightNumbers[i]);

            Console.Write("Select flight enter index: ");

            // int.TryParse تتحقق من صحة الإدخال + تتحقق أن الرقم في النطاق الصحيح
            if (!int.TryParse(Console.ReadLine(), out int flightIdx)
                || flightIdx < 0 || flightIdx >= flightNumbers.Length)
            {
                Console.WriteLine("Error: Invalid flight selection.");
                return;
            }

            // اعرض التواريخ المتاحة من الـ List
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

            // *** BUG FIX: لازم  بين الرحلة والتاريخ عشان نقدر نفصلهم بعدين بـ Split
            bookingRecord[ticketId] = flightNumbers[flightIdx] + availableDates[dateIdx];

            int idx = ticketNumbers.IndexOf(ticketId);
            Console.WriteLine("Booking confirmed: ");
            Console.WriteLine("Passenger: " + passengerNames[idx]);
            Console.WriteLine("Ticket ID: " + ticketId);
            Console.WriteLine("Flight: " + flightNumbers[flightIdx]);
            Console.WriteLine("Date: " + availableDates[dateIdx]);
        }

        //عرض تفاصيل الحجز
        static void ViewBookingDetails()
        {
            Console.WriteLine("View Booking Details: ");
            Console.Write("Enter ticket ID: ");
            string ticketId = Console.ReadLine().Trim().ToUpper();

            // تحقق أن التذكرة موجودة
            if (!ticketNumbers.Contains(ticketId))
            {
                Console.WriteLine("Error: Ticket ID not found");
                return;
            }

            // جيب اسم المسافر من نفس الـ index
            int idx = ticketNumbers.IndexOf(ticketId);
            string passengerName = passengerNames[idx];

            // تحقق أن التذكرة مو ملغية
            if (cancelledTickets.Contains(ticketId))
            {
                Console.WriteLine("This ticket has been cancelled: ");
                return;
            }

            // تحقق أن في حجز موجود
            if (!bookingRecord.ContainsKey(ticketId))
            {
                Console.WriteLine("No booking found for this ticket");
                return;
            }

            string[] parts = bookingRecord[ticketId].Split();

            // parts[0] = رقم الرحلة, parts[1] = التاريخ
            Console.WriteLine("Booking Summary: ");
            Console.WriteLine("Passenger: " + passengerName);
            Console.WriteLine("Ticket ID: " + ticketId);
            Console.WriteLine("Flight: " + parts[0]);
            Console.WriteLine("Date: " + parts[1]);
        }

        //تحديث الحجز
        static void UpdateBooking()
        {
            Console.WriteLine("Update a Booking: ");
            Console.Write("Enter ticket ID: ");
            string ticketId = Console.ReadLine().Trim().ToUpper();

            // تحقق من الشروط الثلاثة قبل التحديث
            if (!ticketNumbers.Contains(ticketId))
            {
                Console.WriteLine("Error: Ticket ID not found!");
                return;
            }
            if (cancelledTickets.Contains(ticketId))
            {
                Console.WriteLine("Error: Ticket is cancelled");
                return;
            }
            if (!bookingRecord.ContainsKey(ticketId))
            {
                Console.WriteLine("Error: No existing booking found!");
                return;
            }

            string[] parts = bookingRecord[ticketId].Split();
            string currentFlight = parts[0];
            string currentDate = parts[1];

            Console.WriteLine("Current booking:");
            Console.WriteLine("Flight: " + currentFlight + "Date: " + currentDate);

            Console.WriteLine("1.Change flight only");
            Console.WriteLine("2.Change date only");
            Console.WriteLine("3.Change both");
            Console.WriteLine("0.Cancel update");
            Console.Write("Select option: ");

            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Invalid input: ");
                return;
            }

            if (option == 0)
            {
                Console.WriteLine("No changes made: ");
                return;
            }

            // نبدأ بالقيم الحالية وغير حسب الاختيار
            string newFlight = currentFlight;
            string newDate = currentDate;

            // غير الرحلة لو اختار 1 أو 3
            if (option == 1 || option == 3)
            {
                Console.WriteLine("Available Flights: ");
                for (int i = 0; i < flightNumbers.Length; i++)
                    Console.WriteLine(i + flightNumbers[i]);
                Console.Write("Select new flight: ");
                if (!int.TryParse(Console.ReadLine(), out int fi) || fi < 0 || fi >= flightNumbers.Length)
                {
                    Console.WriteLine("Invalid flight selection Update cancelled: ");
                    return;
                }
                newFlight = flightNumbers[fi];
            }

            // غير التاريخ لو اختار 2 أو 3
            if (option == 2 || option == 3)
            {
                Console.WriteLine("Available Dates:");
                for (int i = 0; i < availableDates.Count; i++)
                    Console.WriteLine(i + availableDates[i]);
                Console.Write("Select new date: ");
                if (!int.TryParse(Console.ReadLine(), out int di) || di < 0 || di >= availableDates.Count)
                {
                    Console.WriteLine("Invalid date selection. Update cancelled");
                    return;
                }
                newDate = availableDates[di];
            }


            bookingRecord[ticketId] = newFlight + newDate;

            Console.WriteLine("Booking updated successfully");
            Console.WriteLine("Old: Flight " + currentFlight + "Date " + currentDate);
            Console.WriteLine("New: Flight " + newFlight + "Date " + newDate);
        }

        //إلغاء التذكرة
        static void CancelTicket()
        {
            Console.WriteLine("Cancel a Ticket: ");
            Console.Write("Enter ticket ID: ");
            string ticketId = Console.ReadLine().Trim().ToUpper();

            if (!ticketNumbers.Contains(ticketId))
            {
                Console.WriteLine("Error: Ticket ID not found");
                return;
            }
            if (cancelledTickets.Contains(ticketId))
            {
                Console.WriteLine("Error: Ticket is already cancelled");
                return;
            }

            // جيب اسم المسافر من الـ index المطابق
            int idx = ticketNumbers.IndexOf(ticketId);
            string passengerName = passengerNames[idx];

            // احذف الحجز من الـ Dictionary لو موجود
            if (bookingRecord.ContainsKey(ticketId))
            {
                Console.WriteLine("Booking removed: " + bookingRecord[ticketId]);
                bookingRecord.Remove(ticketId);// Remove تحذف بالمفتاح
            }

            // أضف للقائمة الملغية
            cancelledTickets.Add(ticketId);

            if (checkedInQueue.Contains(passengerName))
            {
                Queue<string> tempQueue = new Queue<string>();
                while (checkedInQueue.Count > 0)
                {
                    string p = checkedInQueue.Dequeue();
                    if (p != passengerName)
                        tempQueue.Enqueue(p);
                }
                while (tempQueue.Count > 0)
                    checkedInQueue.Enqueue(tempQueue.Dequeue());

                Console.WriteLine(passengerName + "removed from check in queue: ");
            }

            if (boardingStack.Contains(passengerName))
            {
                Stack<string> tempStack = new Stack<string>();
                while (boardingStack.Count > 0)
                {
                    string p = boardingStack.Pop();
                    if (p != passengerName)
                        tempStack.Push(p);
                }
                while (tempStack.Count > 0)
                    boardingStack.Push(tempStack.Pop());

                Console.WriteLine(passengerName + "removed from boarding stack: ");
            }

            Console.WriteLine("Cancellation complete");
            Console.WriteLine("Ticket: " + ticketId + "Passenger: " + passengerName + "Status: CANCELLED");
        }


        //تسجيل الدخول للرحلة

        static void PassengerCheckIn()
        {
            int option = -1;
            while (option != 0)
            {
                Console.WriteLine("Passenger Check In: ");
                Console.WriteLine("1.Check in a passenger: ");
                Console.WriteLine("2.View check in queue: ");
                Console.WriteLine("3.Process next passenger: ");
                Console.WriteLine("0.Back");
                Console.Write("Select: ");

                if (!int.TryParse(Console.ReadLine(), out option)) ;
                {
                    Console.WriteLine("Invalid input.");
                    return;

                }

            }
        

            if (option == 1)
            {
                Console.Write("Enter ticket ID: ");
                string ticketId = Console.ReadLine().Trim().ToUpper();

                if (!ticketNumbers.Contains(ticketId))
                {
                    Console.WriteLine("Error: Ticket not found.");
                    return;
                }
                if (cancelledTickets.Contains(ticketId))
                {
                    Console.WriteLine("Error: Ticket is cancelled.");
                    return;

                }
                if (!bookingRecord.ContainsKey(ticketId))
                {
                    Console.WriteLine("Error: No booking found. Please book first option 3)");
                    return;
                }

                int idx = ticketNumbers.IndexOf(ticketId);
                string passengerName = passengerNames[idx];

                // Contains يتحقق أن المسافر مو موجود أصلاً في الـ queue
                if (checkedInQueue.Contains(passengerName))
                {
                    Console.WriteLine("Error: Passenger already in check-in queue");
                    return;
                }

                // لو عدد الـ queue أقل من 10 أضفه فيها وإلا أضفه في الانتظار
                if (checkedInQueue.Count < 10)
                {
                    checkedInQueue.Enqueue(passengerName);
                    Console.WriteLine(passengerName + " checked in. Queue position: " + checkedInQueue.Count);
                }
                else
                {
                    waitlistQueue.Enqueue(passengerName);
                    Console.WriteLine("Queue is full. " + passengerName + " added to waitlist (position " + waitlistQueue.Count + ").");
                }
            }
            else if (option == 2)
            {
                Console.WriteLine("Check-In Queue: ");
                if (checkedInQueue.Count == 0)
                {
                    Console.WriteLine("Queue is empty.");
                }
                else
                {
                    // foreach تعرض بدون ما تحذف من الـ queue
                    int pos = 1;
                    foreach (string p in checkedInQueue)
                    {
                        Console.WriteLine(pos + ". " + p);
                        pos++;
                    }
                }
                Console.WriteLine("Waitlist count: " + waitlistQueue.Count);
            }
            else if (option == 3)
            {
                if (checkedInQueue.Count == 0)
                {
                    Console.WriteLine("Queue is empty. No passengers to process.");
                    return;
                }

                // Dequeue تأخذ الشخص الأول من الـ queue
                string processed = checkedInQueue.Dequeue();
                Console.WriteLine("Processed: " + processed);

                // لو في أشخاص في الانتظار انقلهم للـ queue تلقائياً
                if (waitlistQueue.Count > 0)
                {
                    string promoted = waitlistQueue.Dequeue();
                    checkedInQueue.Enqueue(promoted);
                    Console.WriteLine(promoted + " moved from waitlist to check in queue: ");

                    try
                    {
                        int divisor = 0;
                        int result = 10 / divisor;
                        Console.WriteLine(result);
                    }
                    catch (DivideByZeroException ex)
                    { Console.WriteLine("Error:" + ex.Message); }
                }

            }
            
        }
    }
}

        


