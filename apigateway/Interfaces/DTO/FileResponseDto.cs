namespace Common.DTO
{
    public class ResponseFileDto
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public List<FileResponseDto> ResponseDtos { get; set; }
    }

    public class FileResponseDto
    {
        public byte[] FileBytes { get; set; }
    }

}
