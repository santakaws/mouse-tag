using System;
using System.Windows.Forms;

public class mouseTagMain {
    static void Main(string[] args) {
        System.Console.WriteLine("Welcome to the Main method of the Mouse Tag program.");
        mouseTagUI tag = new mouseTagUI();
        Application.Run(tag);
        System.Console.WriteLine("Main method will now shutdown.");
    }
}