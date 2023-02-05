using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace IntegrationTests.requestBuilders
{
    public class HttpStringContentBuilder<T>
    {
        private string content;
        private string mediaType;
        private Encoding encoding;

        public HttpStringContentBuilder<T> WithContent(T item)
        {
            this.content = JsonSerializer.Serialize(item);
            return this;
        }

        public HttpStringContentBuilder<T> WithMediaTypeAplicationJson()
        {
            this.mediaType = MediaTypeNames.Application.Json;
            return this;
        }

        public HttpStringContentBuilder<T> WithEndocdingUTF8()
        {
            this.encoding = Encoding.UTF8;
            return this;
        }

        public StringContent Create()
        {
            return new (this.content, this.encoding, this.mediaType);
        }

    }
}
