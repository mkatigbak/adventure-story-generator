using System;
using System.Collections.Generic;
using System.IO;

namespace Adventure_Story_Generator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* ADVENTURE STORY GENERATOR - MARK KATIGBAK */


            // GET THE FOLDER WHERE THIS PROGRAM IS LOCATED
            string directory = AppDomain.CurrentDomain.BaseDirectory;

            // READ THE STORY TEMPLATE FROM A TEXT FILE
            string template = ReadTemplate(@"C:\Users\marka\source\repos\Adventure_Story_Generator\Adventure_Story_Generator\story_template.txt");

            // ASK THE USER FOR INFORMATION TO FILL IN THE STORY
            Dictionary<string, string> userInputs = GetUserInputs(template);

            // CREATE THE FINAL STORY BY REPLACING PLACEHOLDERS WITH USER INFORMATION
            string story = GenerateStory(template, userInputs);

            // ASK THE USER FOR A NAME TO SAVE THE STORY
            Console.Write("Enter a filename to save your story: ");
            string filename = Console.ReadLine();                   // GET THE FILENAME FROM THE USER
            SaveStory(story, Path.Combine(directory, filename));    // SAVE THE STORY TO A FILE

            // INFORM THE USER THAT THE STORY HAS BEEN SAVED SUCCESSFULLY
            Console.WriteLine($"The story is successfully saved to {Path.Combine(directory, filename)}");
        }

        // THIS FUNCTION READS THE STORY TEMPLATE FROM A FILE
        static string ReadTemplate(string filename)
        {
            try
            {
                return File.ReadAllText(filename);  // READ ALL THE TEXT FROM THE FILE
            }
            catch (Exception)   // IF THERE IS AN ERROR
            {
                throw new Exception("Error reading the story template file.");  // SHOW AN ERROR MESSAGE
            }
        }

        // THIS FUNCTION ASKS THE USER FOR INFORMATION TO FILL IN THE STORY
        static Dictionary<string, string> GetUserInputs(string template)
        {
            // CREATE A LIST OF PLACEHOLDERS AND THEIR FRIENDLY NAMES
            var placeholders = new Dictionary<string, string>
            {
                { "{location}", "location" },               // PLACEHOLDER FOR THE LOCATION
                { "{character_name}", "character name" },   // PLACEHOLDER FOR THE CHARACTER'S NAME
                { "{item}", "item" },                       // PLACEHOLDER FOR THE ITEM
                { "{action}", "action" }                    // PLACEHOLDER FOR THE ACTION
            };

            // THIS WILL STORE THE USER'S ANSWERS
            var userInputs = new Dictionary<string, string>();

            // LOOP THROUGH EACH PLACEHOLDER TO GET USER INPUT
            foreach (var placeholder in placeholders)
            {
                // DECIDE WHETHER TO USE "a" OR "an" BASED ON THE FIRST LETTER
                string article = "a";   // START WITH "a"
                if ("aeiou".Contains(placeholder.Value[0].ToString().ToLower()))    // CHECK IF IT STARTS WITH A VOWEL
                {
                    article = "an"; // CHANGE TO "an" IF IT STARTS WITH A VOWEL
                }

                // ASK THE USER FOR INPUT
                Console.Write($"Enter {article} {placeholder.Value}: ");    // SHOW THE PROMPT
                string input = Console.ReadLine();                          // GET THE USER'S INPUT

                // MAKE SURE ITEM AND ACTION INPUTS ARE IN LOWERCASE
                if (placeholder.Key == "{item}" || placeholder.Key == "{action}")
                {
                    input = input.ToLower();    // CONVERT TO LOWERCASE
                }
                // CAPITALIZE THE FIRST LETTER FOR LOCATION AND CHARACTER NAME
                else if (placeholder.Key == "{location}" || placeholder.Key == "{character_name}")
                {
                    input = char.ToUpper(input[0]) + input.Substring(1);    // Capitalize the first letter
                }

                userInputs[placeholder.Key] = input;    // STORE THE USER'S INPUT
            }

            return userInputs;  // RETURN THE USER'S INPUTS
        }

        // THIS FUNCTION CREATES THE FINAL STORY BY REPLACING PLACEHOLDERS WITH USER INPUTS
        static string GenerateStory(string template, Dictionary<string, string> userInputs)
        {
            string story = template;    // START WITH THE TEMPLATE
            foreach (var input in userInputs)   // GO THROUGH EACH USER INPUT
            {
                story = story.Replace(input.Key, input.Value);  // REPLACE THE PLACEHOLDER WITH THE USER'S INPUT
            }
            return story;   // RETURN THE COMPLETED STORY
        }

        // THIS FUNCTION SAVES THE STORY TO A FILE
        static void SaveStory(string story, string filename)
        {
            try
            {
                File.WriteAllText(filename, story); // WRITE THE STORY TO THE FILE
            }
            catch (Exception)   // IF THERE IS AN ERROR
            {
                throw new Exception("Error saving the story to a file.");   // SHOW AN ERROR MESSAGE
            }
        }
    }
}