using System;
using System.Text.RegularExpressions;

public class ISBNValidator
{
    public static bool ValidateISBN(string isbn)
    {
        // Remove any non-numeric characters from the ISBN
        string cleanedISBN = Regex.Replace(isbn, @"[^0-9]", "");

        // Check if the cleaned ISBN is of valid length (10 or 13 digits)
        if (cleanedISBN.Length != 10 && cleanedISBN.Length != 13)
        {
            return false;
        }

        // Validate ISBN-10 or ISBN-13 based on length
        if (cleanedISBN.Length == 10)
        {
            return ValidateISBN10(cleanedISBN);
        }
        else // cleanedISBN.Length == 13
        {
            return ValidateISBN13(cleanedISBN);
        }
    }

    private static bool ValidateISBN10(string isbn10)
    {
        int checksum = 0;

        for (int i = 0; i < 9; i++)
        {
            checksum += (i + 1) * (int)Char.GetNumericValue(isbn10[i]);
        }

        checksum %= 11;
        char lastDigit = isbn10[9];

        if ((checksum == 10 && lastDigit == 'X') || (checksum == (int)Char.GetNumericValue(lastDigit)))
        {
            return true;
        }

        return false;
    }

    private static bool ValidateISBN13(string isbn13)
    {
        int checksum = 0;

        for (int i = 0; i < 12; i++)
        {
            int digit = (int)Char.GetNumericValue(isbn13[i]);
            checksum += (i % 2 == 0) ? digit : digit * 3;
        }

        checksum = (10 - (checksum % 10)) % 10;
        char lastDigit = isbn13[12];

        if (checksum == (int)Char.GetNumericValue(lastDigit))
        {
            return true;
        }

        return false;
    }

}
