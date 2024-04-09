using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeleteSessions
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {


                using (var ctx = new Entities())
                {
                    var list = ctx.CURRENT_SESSION.ToList();

                    foreach (var currentSession in list)
                    {
                        ctx.CURRENT_SESSION.DeleteObject(currentSession);
                    }
                    ctx.SaveChanges();
                }
                Console.WriteLine("Succsess");
            }
            catch (Exception ex)
            {

                Console.WriteLine(
                "Error  " + ex.Message);
            }
            Console.WriteLine("Finish");
            Console.Read();
        }
    }
}
