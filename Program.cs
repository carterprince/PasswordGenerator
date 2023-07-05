using System;
using TextCopy;

class Program {
    // generates a random password of length
    static string GeneratePassword(int length) {
        string password = "";

        string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string lowerCase = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "0123456789";
        string special = "!@#$%^&*()_+";
        string all = upperCase + lowerCase + numbers + special;

        Random rand = new Random();

        // at least 1 random upper, lower, number, special, if the length permits it
        switch (length) {
            case 1:
                password += upperCase[rand.Next(upperCase.Length)];
                break;
            case 2:
                password += upperCase[rand.Next(upperCase.Length)];
                password += lowerCase[rand.Next(lowerCase.Length)];
                break;
            case 3:
                password += upperCase[rand.Next(upperCase.Length)];
                password += lowerCase[rand.Next(lowerCase.Length)];
                password += numbers[rand.Next(numbers.Length)];
                break;
            default:
                password += upperCase[rand.Next(upperCase.Length)];
                password += lowerCase[rand.Next(lowerCase.Length)];
                password += numbers[rand.Next(numbers.Length)];
                password += special[rand.Next(special.Length)];
                break;
        }
        
        // generate rest of the password
        while (password.Length < length) {
            password += all[rand.Next(all.Length)];
        }

        // simple one-liner to scramble the password
        return new string(password.OrderBy(x => rand.Next()).ToArray());
    }

	// generates a random sentence of length
	// takes a delimeter, default is space
	static string GenerateSentence(int length, bool randomizeDelimeter = false) {
		string[] words = System.IO.File.ReadAllLines("/home/carter/words.txt");
		string[] sentence = new string[length];
		string[] delimeters = { " ", "-", "_", ".", ",", ":", ";", "!", "?" };
		string[] special = { "!", "@", "#", "$", "%", "^", "&", "*", "(", ")" };

		Random rand = new Random();

		// generate the sentence
		for (int i = 0; i < length; i++) {
			sentence[i] = words[rand.Next(words.Length)];
		}

		// make the first word all uppercase
		sentence[0] = sentence[0].ToUpper();

		// add a number to the second word
		sentence[1] += rand.Next(100);

		// add a special character to the third word
		sentence[2] += special[rand.Next(special.Length)];

		// scramble the array
		sentence = sentence.OrderBy(x => rand.Next()).ToArray();

		// join the array into a string
		if (randomizeDelimeter) {
			// we need to randomize the delimeter
			for (int i = 0; i < sentence.Length - 1; i++) {
				sentence[i] += delimeters[rand.Next(delimeters.Length)];
			}
			return String.Join("", sentence);
		} else {
			return String.Join(" ", sentence);
		}
	}

    static void Main(string[] args) {
		// if args is empty, use default length of 16
		int length;
		if (args.Length > 0) {
			length = Int32.Parse(args[0]);
		} else {
			length = 16;
		}
        string password = GenerateSentence(length, true);
        Console.WriteLine("Password: " + password);
        ClipboardService.SetText(password);
        Console.WriteLine("Password copied to clipboard.");
    }
}
