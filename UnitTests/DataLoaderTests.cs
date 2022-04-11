using AnkhMorpork;
using NUnit.Framework;
using Moq;
using System;
using System.IO;

namespace UnitTests
{
    [TestFixture]
    public class DataLoaderTests
    {
        private DataLoader _dataLoader;
        private Mock<IFileReader> _fileReader;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _dataLoader = new DataLoader(_fileReader.Object);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void LoadNpcsFromJson_WrongFilePath_ReturnError(string path)
        {
            _fileReader.Setup(fr => fr.Read(path))
                       .Throws<FileNotFoundException>();

            Assert.That(() => _dataLoader.LoadNpcsFromJson(path), 
                              Throws.Exception.TypeOf<FileNotFoundException>());
        }

        [Test]
        public void LoadNpcsFromJson_EmptyFile_ReturnError()
        {
            _fileReader.Setup(fr => 
                fr.Read(It.IsAny<string>()))
                  .Returns(String.Empty);

            Assert.That(() => _dataLoader.LoadNpcsFromJson(It.IsAny<string>()), 
                              Throws.ArgumentException);
        }
    }
}
