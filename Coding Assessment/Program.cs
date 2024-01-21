using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Coding_Assessment
{
    class Flight
    {
        public int Number { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public int Day { get; set; }
    }
    class Order
    {
        public string OrderId { get; set; }
        public string Destination { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {

            //User Story 1

            try
            {
                // Read the flight schedule from a JSON file
                string json = System.IO.File.ReadAllText("D:\\Assessment\\coding-assigment-orders.json");
                JObject jsonObject = JObject.Parse(json);

                // Extract flights from the JSON object
                List<Flight> flights = jsonObject.Properties()
                    .Where(p => p.Value["destination"] != null)
                    .Select(p => new Flight
                    {
                        Number = int.Parse(p.Name.Replace("order-", "")),
                        Departure = "YUL",  // Assuming YUL as the default departure
                        Arrival = p.Value["destination"].ToString(),
                        Day = (int.Parse(p.Name.Replace("order-", "")) % 2) + 1
                    })
                    .ToList();

                // Check if any flights are found
                if (flights.Count > 0)
                {
                    // Display the loaded flight schedule on the console
                    Console.WriteLine("Loaded Flight Schedule:");
                    foreach (var flight in flights)
                    {
                        Console.WriteLine($"Flight: {flight.Number}, departure: {flight.Departure}, arrival: {flight.Arrival}, day: {flight.Day}");
                    }
                }
                else
                {
                    Console.WriteLine("No flights found in the schedule.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}

