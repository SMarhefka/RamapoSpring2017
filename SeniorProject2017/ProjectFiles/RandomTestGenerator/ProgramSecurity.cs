using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography;

namespace RandomTestGenerator
{
    class ProgramSecurity
    {
        // Define supported password characters divided into groups.
        // You can add (or remove) characters to (from) these groups.
        private static string m_chars_lcase = "abcdefgijkmnopqrstwxyz";
        private static string m_chars_ucase = "ABCDEFGHJKLMNPQRSTWXYZ";
        private static string m_chars_number = "1234567890";
        private static string m_chars_special = "*$-+?_&=!%{}/";

        /**/
        /*
        static public string generatePassword()

        NAME
            generatePassword - process that determines provides information.

        SYNOPSIS
            string generatePassword()
                This function does not take in any arguements

        DESCRIPTION
            This program was taken from http://www.obviex.com/samples/password.aspx.
            I then modified the code to work with my project but a majority
            of the code remains in the original format   

        RETURNS
            This function returns a string

        AUTHOR
                Svetlana Marhefka

        DATE
                6:27pm 3/29/2017

        */
        /**/
        public static string generatePassword()
        {
            int minLength = 20;
            int maxLength = 25;

            // Create a local array containing supported password characters
            // grouped by types. You can remove character groups from this
            // array, but doing so will weaken the password strength.
            char[][] charGroups = new char[][]
            {
                m_chars_lcase.ToCharArray(), m_chars_ucase.ToCharArray(),
                m_chars_number.ToCharArray(), m_chars_special.ToCharArray()
            };

            // Use this array to track the number of unused characters in each
            // character group.
            int[] charsLeftInGroup = new int[charGroups.Length];

            // Initially, all characters in each group are not used.
            for ( int i = 0; i < charsLeftInGroup.Length; i++ )
            {
                charsLeftInGroup[i] = charGroups[i].Length;
            }
            // Use this array to track (iterate through) unused character groups.
            int[] leftGroupsOrder = new int[charGroups.Length];

            // Initially, all character groups are not used.
            for ( int i = 0; i < leftGroupsOrder.Length; i++ )
            {
                leftGroupsOrder[i] = i;
            }

            // Because we cannot use the default randomizer, which is based on 
            // the current time (it will produce the same "random" number within
            // a second), we will use a random number generator to seed the
            // randomizer.

            // Use a 4-byte array to fill it with random bytes and convert it then
            // to an integer value.
            byte[] randomBytes = new byte[5];

            // Generate 4 random bytes.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes( randomBytes );

            // Convert 4 bytes into a 32-bit integer value.
            int byteToBit = BitConverter.ToInt32( randomBytes, 0 );

            // Now, this is real randomization.
            Random random = new Random( byteToBit );

            // This array will hold password characters.
            char[] password = null;

            // Allocate appropriate memory for the password.
            password = new char[random.Next( minLength, maxLength + 1 )];

            // Index of the next character to be added to password.
            int nextCharIdx;

            // Index of the next character group to be processed.
            int nextGroupIdx;

            // Index which will be used to track not processed character groups.
            int nextLeftGroupsOrderIdx;

            // Index of the last non-processed character in a group.
            int lastCharIdx;

            // Index of the last non-processed group.
            int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

            // Generate password characters one at a time.
            for ( int i = 0; i < password.Length; i++ )
            {
                // If only one character group remained unprocessed, process it;
                // otherwise, pick a random character group from the unprocessed
                // group list. To allow a special character to appear in the
                // first position, increment the second parameter of the Next
                // function call by one, i.e. lastLeftGroupsOrderIdx + 1.
                if (lastLeftGroupsOrderIdx == 0)
                {
                    nextLeftGroupsOrderIdx = 0;
                }
                else
                {
                    nextLeftGroupsOrderIdx = random.Next( 0, lastLeftGroupsOrderIdx );
                }

                // Get the actual index of the character group, from which we will
                // pick the next character.
                nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

                // Get the index of the last unprocessed characters in this group.
                lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                // If only one unprocessed character is left, pick it; otherwise,
                // get a random character from the unused character list.
                if (lastCharIdx == 0)
                {
                    nextCharIdx = 0;
                }
                else
                {
                    nextCharIdx = random.Next( 0, lastCharIdx + 1 );
                }

                // Add this character to the password.
                password[i] = charGroups[nextGroupIdx][nextCharIdx];

                // If we processed the last character in this group, start over.
                if (lastCharIdx == 0)
                {
                    charsLeftInGroup[nextGroupIdx] = charGroups[nextGroupIdx].Length;
                }
                // There are more unprocessed characters left.
                else
                {
                    // Swap processed character with the last unprocessed character
                    // so that we don't pick it until we process all characters in
                    // this group.
                    if (lastCharIdx != nextCharIdx)
                    {
                        char temp = charGroups[nextGroupIdx][lastCharIdx];
                        charGroups[nextGroupIdx][lastCharIdx] =
                        charGroups[nextGroupIdx][nextCharIdx];
                        charGroups[nextGroupIdx][nextCharIdx] = temp;
                    }
                    // Decrement the number of unprocessed characters in
                    // this group.
                    charsLeftInGroup[nextGroupIdx]--;
                }

                // If we processed the last group, start all over.
                if (lastLeftGroupsOrderIdx == 0)
                {
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                }
                // There are more unprocessed groups left.
                else
                {
                    // Swap processed group with the last unprocessed group
                    // so that we don't pick it until we process all groups.
                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] =
                        leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }
                    // Decrement the number of unprocessed groups.
                    lastLeftGroupsOrderIdx--;
                }
            }

            // Convert password characters into a string and return the result.
            return new string( password );
        }

        /**/
        /*
        public static String generateSalt()

        NAME
            generateSalt - process that provides information.

        SYNOPSIS
            string generateSalt()
                This function does not take in any arguements

        DESCRIPTION
            This program was taken from http://www.obviex.com/samples/password.aspx.
            I then modified the code to work with my project but a majority
            of the code remains in the original format   

        RETURNS
            This function returns a string

        AUTHOR
                Svetlana Marhefka

        DATE
                6:27pm 3/29/2017

        */
        /**/
        public static String generateSalt()
        {
            byte[] saltBytes;
            // Define min and max salt sizes.
            int minSaltSize = 10;
            int maxSaltSize = 15;

            // Generate a random number for the size of the salt.
            Random random = new Random();
            int saltSize = random.Next( minSaltSize, maxSaltSize );

            // Allocate a byte array, which will hold the salt.
            saltBytes = new byte[saltSize];

            // Initialize a random number generator.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            // Fill the salt with cryptographically strong byte values.
            rng.GetNonZeroBytes( saltBytes );

            string genSaltValue = Convert.ToBase64String( saltBytes );
            // return the generated SaltValue
            return genSaltValue;
        }

        /**/
        /*
        public static string hashPassword( string inputPassword, string inSaltValue )

        NAME
            hashPassword - process that provides information.

        SYNOPSIS
            string hashPassword( string inputPassword, string inSaltValue )

        DESCRIPTION
            This program was taken from http://www.obviex.com/samples/password.aspx.
            I then modified the code to work with my project but a majority
            of the code remains in the original format   

        RETURNS
            This function returns a string

        AUTHOR
            Svetlana Marhefka

        DATE
            6:27pm 3/29/2017

        */
        /**/
        public static string hashPassword( string inputPassword, string inSaltValue )
        {
            byte[] saltBytes = Convert.FromBase64String( inSaltValue );

            // Convert plain text into a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes( inputPassword );

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + saltBytes.Length];

            // Copy plain text bytes into resulting array.
            for ( int i = 0; i < plainTextBytes.Length; i++ )
            {
                plainTextWithSaltBytes[i] = plainTextBytes[i];
            }

            // Append salt bytes to the resulting array.
            for ( int i = 0; i < saltBytes.Length; i++ )
            {
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];
            }

            SHA512CryptoServiceProvider hashingAlgorithm = new SHA512CryptoServiceProvider();
            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hashingAlgorithm.ComputeHash( plainTextWithSaltBytes );

            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length + saltBytes.Length];

            // Copy hash bytes into resulting array.
            for ( int i = 0; i < hashBytes.Length; i++ )
            {
                hashWithSaltBytes[i] = hashBytes[i];
            }
            // Append salt bytes to the result.
            for ( int i = 0; i < saltBytes.Length; i++ )
            {
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];
            }
            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String( hashWithSaltBytes );

            // Return the result.
            return hashValue;
        }
    }
}