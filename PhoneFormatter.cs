using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace C_Sharp_Solution{

    public class PhoneFormatter{

       //phoneFormat reads in a CSV file, reformats the phone field, and writes to a new CSV file
    public void phoneFormat(string filepath) {

      if(filepath == ""){
        throw new InvalidDataException("Filepath may not be empty");
      }

      if(!File.Exists(@filepath)){
        throw new InvalidDataException("Input file does not exist");
      }

      //Prevent commas enclosed in double quotes from registering as new fields
      Regex regex = new Regex("," + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
      string newFile = filepath.Substring(0, filepath.Length-4) + "_REFORMATTED.csv";
      InitCSV(newFile);

      try {
        //Read all Rows
        string[] lines = System.IO.File.ReadAllLines(@filepath);
        for (int i = 1; i < lines.Length; i++) {
          //Split lines into columns
          string[] fields = regex.Split(lines[i]);
          //Format Phone field
          fields[2] = formatInternational(fields[2]);
          //Add current row to new file
          addRow(fields, @newFile);
        }
        Console.WriteLine("Phone Format Complete: " + filepath);
      }
      catch(Exception ex) {
        throw new ApplicationException("phoneFormat Error", ex);
      }
    }

        //addRow adds a new row
    public static void addRow(string[] fields, string filepath) {

      try {
        using(System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true)) {

          file.Write(fields[0]);
          for (int i = 1; i < fields.Length; i++) {
            file.Write("," + fields[i]);
          }
          file.WriteLine();
        }
      }
      catch(Exception ex) {
        throw new ApplicationException("addRow Error", ex);
      }
    }

    public string formatInternational(string number) {
        //Check for valid length for either Irish or International formats
        if(number.Length != 10 && number.Length != 13){
          throw new InvalidOperationException("Invalid Phone Number Length.");
        }
        //If number starts with "08", denoting it is an Irish number, reformat it to International
        else if (number.StartsWith("08")) {   
            //Reformat and the phone number
            number = "+353" + number.Substring(1);
            return number;
      }

      //Else return the phone number unchanged
      return number;
    }

    //Initialise CSV file by adding column names to the first line, overwriting if the file already exists
    public void InitCSV(string filepath){
         using(System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, false)) {
          file.WriteLine("Name,Address,Phone,Title,Company,ID");
        }
    }

    }
}