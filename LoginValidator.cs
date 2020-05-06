using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PI {
    public abstract class LoginValidator {
        protected LoginValidator nextHandler;
        protected List<string> ErrorList;

        protected LoginValidator() {
            ErrorList = new List<string>();
        }

        public void SetNextHandler(LoginValidator handler) {
            this.nextHandler = handler;
        }

        public abstract List<string> HandleRequest(Login page);
    }

    class AlbumNumberPresent : LoginValidator {
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

    class AlbumNumberValid : LoginValidator {
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

    class EmailPresent : LoginValidator {
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

    class EmailValid : LoginValidator {
        public override List<string> HandleRequest(Login page) {
            if (!isValid(page.getEmail())) {
                ErrorList.Add("Email musi spelniac wymogi (@/domena etc.)!!!");
            }
            else if (nextHandler != null) {
                ErrorList = nextHandler.HandleRequest(page);
            }
            return ErrorList;
        }

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