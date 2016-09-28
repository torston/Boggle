using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Boggle.Tests.Tests
{
    [TestFixture]
    public class ValidatorTests
    {
        [Test]
        public void validate_board_smaller_that_3x3_exception()
        {
            var validator = new Validator();
            var board = new char[2, 2];

            validator.Invoking(r => r.ValidateBoard(board))
                .ShouldThrow<ArgumentException>()
                .WithMessage("Board size should be grater or equals 3x3");
        }

        [Test]
        public void validate_board_null_exception()
        {
            var validator = new Validator();

            validator.Invoking(r => r.ValidateBoard(null))
                .ShouldThrow<ArgumentException>()
                .WithMessage("Board can not be null");
        }

        [Test]
        public void validate_board_have_non_english_characters()
        {
            var validator = new Validator();

            var board = new char[3, 3];

            board[0, 0] = '1';

            validator.Invoking(r => r.ValidateBoard(board))
                .ShouldThrow<ArgumentException>()
                .WithMessage("Board have non english character: '1', support only english characters");
        }

        [Test]
        public void happy_path_board_validation()
        {
            var validator = new Validator();

            var board = new char[3, 3];

            for (int i = 0; i <= board.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= board.GetUpperBound(1); j++)
                {
                    board[i, j] = 'a';
                }
            }

            validator.Invoking(r => r.ValidateBoard(board))
                .ShouldNotThrow<ArgumentException>();
        }

        [Test]
        public void validate_dictionary_have_non_english_characters()
        {
            var validator = new Validator();

            var loadedWords = new HashSet<string>();
            loadedWords.Add("abc1");

            validator.Invoking(r => r.ValidateDictionary(loadedWords))
                .ShouldThrow<ArgumentException>()
                .WithMessage("Dictionary word have non english character: '1' in word abc1, support only english characters");
        }

        [Test]
        public void happy_path_dictionary_validations()
        {
            var validator = new Validator();

            var loadedWords = new HashSet<string>();

            loadedWords.Add("hat");
            loadedWords.Add("row");
            loadedWords.Add("table");

            validator.Invoking(r => r.ValidateDictionary(loadedWords))
                .ShouldNotThrow<ArgumentException>();
        }
    }
}
