namespace Features.JsonModule.Core
{
    public enum ErrorType
    {
        // common
        UnsupportedPlatform,
        InvalidFilePath,

        // for load
        TwiceLoaded,
        FileReadException,

        // for save
        WasNoFileReading,
        PotentialLoopOverflow,
        FileWriteException,
        
        TODO,
    }
}