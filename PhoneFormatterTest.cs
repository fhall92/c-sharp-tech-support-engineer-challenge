using System;
using Xunit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace C_Sharp_Solution
{
    public class PhoneFormatterTest
    {
       private readonly PhoneFormatter formatter = new PhoneFormatter();

            [Fact]
            public void formatIrishToInternational(){
               string testValueIrish = "0871111111";

               Assert.Equal("+353871111111", formatter.formatInternational(testValueIrish));
            }

            [Fact]
            public void formatInternationalValueUnchanged(){
               string testValueInternational = "+353872222222";

               Assert.Equal("+353872222222", formatter.formatInternational(testValueInternational));
            }

            [Fact]
            public void irishPhoneNumberWrongLength(){
               string testValue = "08511";

               var ex = Assert.Throws<InvalidOperationException>(() => formatter.formatInternational(testValue));

               Assert.Equal("Invalid Phone Number Length.", ex.Message);
            }

            [Fact]
            public void internationalPhoneNumberWrongLength(){
               string testValue = "+35385111111111111111111111";

               var ex = Assert.Throws<InvalidOperationException>(() => formatter.formatInternational(testValue));

               Assert.Equal("Invalid Phone Number Length.", ex.Message);
            }

            [Fact]
            public void phoneFormatNewFileExists(){
               formatter.phoneFormat("benchmark_seamless_infrastructures.csv");

               Assert.Equal(true, File.Exists("benchmark_seamless_infrastructures_REFORMATTED.csv"));
            }

            [Fact]
            public void phoneFormatNumberOfRowsMatch(){
               formatter.phoneFormat("engage rich interfaces.csv");
               string[] linesOriginal = System.IO.File.ReadAllLines("engage rich interfaces.csv");
               string[] linesNew = System.IO.File.ReadAllLines("engage rich interfaces_REFORMATTED.csv");
               int originalLength = linesOriginal.Length;
               int newLength = linesNew.Length;

               Assert.Equal(true, originalLength == newLength);
            }

            [Fact]
            public void phoneFormatEmptyFilename(){
               string testValue = "";
               var ex = Assert.Throws<InvalidDataException>(() => formatter.phoneFormat(testValue));

               Assert.Equal("Filepath may not be empty", ex.Message);
            }   

            [Fact]
            public void phoneFormatNonexistantFile(){
               string testValue = "ThisFileDoesNotExist.csv";

               var ex = Assert.Throws<InvalidDataException>(() => formatter.phoneFormat(testValue));

               Assert.Equal("Input file does not exist", ex.Message);
            }
    }
}