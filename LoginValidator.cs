using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PI {
    /// <summary>
    /// Interface for ChainOfValidation pattern
    /// </summary>
    public interface Validator {
        void SetNextHandler(Validator handler);
        List<string> HandleRequest(Login page);
    }


    /// <summary>
    /// Abstract class for for ChainOfValidation pattern
    /// </summary>
    public abstract class LoginValidator : Validator {
        /// <summary>
        /// Next handler to be called in our chain pattern
        /// </summary>
        protected Validator nextHandler;
        /// <summary>
        /// List to store all Errors occured in chain validation
        /// </summary>
        protected List<string> ErrorList;

        /// <summary>
        /// Set ErrorList as new List of strings
        /// </summary>
        protected LoginValidator() {
            ErrorList = new List<string>();
        }

        /// <summary>
        /// Set next handler to be called for our current handler
        /// </summary>
        /// <param name="handler">Next handler</param>
        public void SetNextHandler(Validator handler) {
            this.nextHandler = handler;
        }

        /// <summary>
        /// Abstract method to HandleRequest.
        /// </summary>
        /// <param name="page">Whole page object</param>
        /// <returns>
        /// ErrorList with error occured (if any error occurs)
        /// </returns>
        public abstract List<string> HandleRequest(Login page);
    }

    /// <summary>
    /// Concrete class. For Validation of album number (exist).
    /// </summary>
    class AlbumNumberPresent : LoginValidator {
        /// <summary>
        /// Checks if album number is present in textbox
        /// </summary>
        /// <param name="page">Page with textbox</param>
        /// <returns>ErrorList with error occured (if any error occurs)</returns>
        public override List<string> HandleRequest(Login page) {
            if (page.getAlbum() == null || page.getAlbum() == string.Empty) {
                ErrorList.Add("Numer albumu jest wymagany!!!");
            } 
            else if (nextHandler != null) {
                ErrorList = nextHandler.HandleRequest(page);
            }
            return ErrorList;
        }

    }

    /// <summary>
    /// Concrete class. For Validation of album number (valid).
    /// </summary>
    class AlbumNumberValid : LoginValidator {
        /// <summary>
        /// Checks if album number is valid (is build form six numbers)
        /// </summary>
        /// <param name="page">Page with textbox</param>
        /// <returns>ErrorList with error occured (if any error occurs)</returns>
        public override List<string> HandleRequest(Login page) {
            if (!int.TryParse(page.getAlbum(), out int _) || !(page.getAlbum().Length == 6)) {
                ErrorList.Add("Numer albumu musi skladac sie z 6 cyfr!!!");
            }
            else if (nextHandler != null) {
                ErrorList = nextHandler.HandleRequest(page);
            }
            return ErrorList;
        }
    }

    /// <summary>
    /// Concrete class. For Validation of email (exist).
    /// </summary>
    class EmailPresent : LoginValidator {
        /// <summary>
        /// Checks if email is present in textbox
        /// </summary>
        /// <param name="page">Page with textbox</param>
        /// <returns>ErrorList with error occured (if any error occurs)</returns>
        public override List<string> HandleRequest(Login page) {
            if (page.getEmail() == null || page.getEmail() == string.Empty) {
                ErrorList.Add("Email nie moze byc pusty!!!");
            }
            else if (nextHandler != null) {
                ErrorList = nextHandler.HandleRequest(page);
            }
            return ErrorList;
        }
    }

    /// <summary>
    /// Concrete class. For Validation of email (valid).
    /// </summary>
    class EmailValid : LoginValidator {
        /// <summary>
        /// Checks if email is valid (contains name, host and domain) by callind <c>isValid</c> method
        /// </summary>
        /// <param name="page">Page with textbox</param>
        /// <returns>ErrorList with error occured (if any error occurs)</returns>
        public override List<string> HandleRequest(Login page) {
            if (!isValid(page.getEmail())) {
                ErrorList.Add("Email musi spelniac wymogi (@/domena etc.)!!!");
            }
            else if (nextHandler != null) {
                ErrorList = nextHandler.HandleRequest(page);
            }
            return ErrorList;
        }

        /// <summary>
        /// Check if email address is valid (contains name, host and domain)
        /// </summary>
        /// <param name="email">Email to check</param>
        /// <returns>
        /// True if email is valid
        /// False if email is invalid
        /// </returns>
        bool isValid(string email) {
            try {
                var mail = new System.Net.Mail.MailAddress(email);
                return mail.Address == email;
            }
            catch {
                return false;
            }
        }
    }

}