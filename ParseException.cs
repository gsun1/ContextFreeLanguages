using System;

namespace PE {
    public class ParseException: Exception {
        public ParseException() {}
        public ParseException(string message) : base(message) {}
    }
}