namespace CC_Functions.Misc
{
    /// <summary>
    ///     Characters for use in CC-Functions.CommandLine
    /// </summary>
    public static class SpecialChars
    {
        /// <summary>
        ///     The space character
        /// </summary>
        public const char Empty = ' ';
        /// <summary>
        /// Wall with two lines
        /// </summary>
        public static class TwoLineSimple
        {
            // 1 connectors
            /// <summary>
            /// Wall with specified points
            /// </summary>
            public const char Up = '║';
            /// <summary>
            /// Wall with specified points
            /// </summary>
            public const char Down = '║';
            /// <summary>
            /// Wall with specified points
            /// </summary>
            public const char Left = '═';
            /// <summary>
            /// Wall with specified points
            /// </summary>
            public const char Right = '═';
            
            // 2 connectors
            /// <summary>
            /// Wall with specified points
            /// </summary>
            public const char UpDown = '║';
            /// <summary>
            /// Wall with specified points
            /// </summary>
            public const char LeftRight = '═';
            /// <summary>
            /// Wall with specified points
            /// </summary>
            public const char DownRight = '╔';
            /// <summary>
            /// Wall with specified points
            /// </summary>
            public const char UpRight = '╚';
            /// <summary>
            /// Wall with specified points
            /// </summary>
            public const char DownLeft = '╗';
            /// <summary>
            /// Wall with specified points
            /// </summary>
            public const char UpLeft = '╝';

            // 3 connectors
            /// <summary>
            /// Wall with specified points
            /// </summary>
            public const char UpDownLeft = '╣';
            /// <summary>
            /// Wall with specified points
            /// </summary>
            public const char UpDownRight = '╠';
            /// <summary>
            /// Wall with specified points
            /// </summary>
            public const char UpLeftRight = '╩';
            /// <summary>
            /// Wall with specified points
            /// </summary>
            public const char DownLeftRight = '╦';

            // 4 connectors
            /// <summary>
            /// Wall with specified points
            /// </summary>
            public const char UpDownLeftRight = '╬';
        }
        /// <summary>
        /// Simple line
        /// </summary>
        public static class OneLineSimple
        {
            // 1 connectors
            /// <summary>
            /// Line with specified points
            /// </summary>
            public const char Up = '╵';
            /// <summary>
            /// Line with specified points
            /// </summary>
            public const char Down = '╷';
            /// <summary>
            /// Line with specified points
            /// </summary>
            public const char Left = '╴';
            /// <summary>
            /// Line with specified points
            /// </summary>
            public const char Right = '╶';

            // 2 connectors
            public const char UpDown = '│';
            /// <summary>
            /// Line with specified points
            /// </summary>
            public const char LeftRight = '─';
            /// <summary>
            /// Line with specified points
            /// </summary>
            public const char DownRight = '┌';
            /// <summary>
            /// Line with specified points
            /// </summary>
            public const char UpRight = '└';
            /// <summary>
            /// Line with specified points
            /// </summary>
            public const char DownLeft = '┐';
            /// <summary>
            /// Line with specified points
            /// </summary>
            public const char UpLeft = '┘';

            // 3 connectors
            /// <summary>
            /// Line with specified points
            /// </summary>
            public const char UpDownLeft = '┤';
            /// <summary>
            /// Line with specified points
            /// </summary>
            public const char UpDownRight = '├';
            /// <summary>
            /// Line with specified points
            /// </summary>
            public const char UpLeftRight = '┴';
            /// <summary>
            /// Line with specified points
            /// </summary>
            public const char DownLeftRight = '┬';

            // 4 connectors
            /// <summary>
            /// Line with specified points
            /// </summary>
            public const char UpDownLeftRight = '┼';
        }
    }
}