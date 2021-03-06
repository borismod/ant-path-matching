﻿using NUnit.Framework;
using System.Text.RegularExpressions;

namespace AntPathMatching
{
    [TestFixture]
    public class AntTests
    {
        [Test]
        public void NullInput_ThrowsNothing()
        {
            var ant = new Ant("*");
            Assert.That(() => ant.IsMatch(null), Throws.Nothing);
        }

        [TestCase("/dir1/**/*.txt", "/dir1/dir2/dir3/file1.zip", false)]
        [TestCase("/dir1/**/*.txt", "/dir1/dir2/dir3/file1.txt", true)]
        [TestCase("/dir1/**/*.txt", "/dir1/dir2/dir3/file1.zip", false)]
        [TestCase("/dir1/dir2/**/*.txt", "/dir1/dir2/dir3/file1.txt", true)]
        [TestCase("/dir1/dir2/**/*.txt", "/dir1/dir3/dir2/file1.txt", false)]
        [TestCase("/dir1/*.txt", "/dir1/file1.txt", true)]
        [TestCase("/dir1/*.txt", "/dir1/file1.zip", false)]
        [TestCase("/dir1/file?.txt", "/dir1/file1.txt", true)]
        [TestCase("/dir1/file?.txt", "/dir1/file12.txt", false)]
        [TestCase("/dir1/file?.*", "/dir1/file1.zip", true)]
        [TestCase("/dir1/file?.*", "/dir1/file12.zip", false)]
        [TestCase("/dir1/*/file.txt", "/dir1/dir1_1/file.txt", true)]
        [TestCase("/dir1/*/file.txt", "/dir1/dir1_1/dir1_2/file.txt", false)]
        [TestCase("/dir1/*/file.txt", "/dir1/dir1_1/dir1_2/file.txtt", false)]
        [TestCase("*.txt", "/dir1/file.txt", false)]
        [TestCase("*.txt", "file.txt", true)]
        [TestCase("*.{txt}", "file.txt", true)]
        [TestCase("*.{txt,zip}", "file.txt", true)]
        [TestCase("*.{txt,zip}", "file.zip", true)]
        [TestCase("*.{txt,zip}", "file.docx", false)]
        [TestCase("/dir1/dir2/dir3/file1.txt", "/dir1/dir2/dir3/file1.txt", true)]
        [TestCase("/dir?/dir2/dir3/file1.txt", "/dir1/dir2/dir3/file1.txt", true)]
        [TestCase("/dir??/dir2/dir3/file1.txt", "/dir1/dir2/dir3/file1.txt", false)]
        [TestCase("/dir??/dir2/dir3/file1.txt", "/dir01/dir2/dir3/file1.txt", true)]
        [TestCase("file1.txt", "file1.txt", true)]
        [TestCase("file??.txt", "file12.txt", true)]
        [TestCase("*.*", "file", false)]
        [TestCase("*", "file", true)]
        [TestCase("**/*", "/dir1/dir2/dir3/file1.txt", true)]
        [TestCase("**", "file", true)]
        [TestCase("**/", "file", true)]
        [TestCase("**/*", "file", true)]
        [TestCase("*/*", "file", false)]
        [TestCase("*", "/dir1/dir2/dir3/file1.txt", false)]
        public void IsMatch_FromTable_ReturnsExpectedValue(
            string pattern,
            string scenario,
            bool expected)
        {
            var ant = new Ant(pattern);
            var match = ant.IsMatch(scenario);

            Assert.That(ant.ToString(), Is.EqualTo(pattern));
            Assert.That(match, Is.EqualTo(expected));
        }

        [Description("See also http://ant.apache.org/manual/dirtasks.html#patterns")]
        // CVS Examples
        [TestCase("**/CVS/*", "CVS/Repository", true)]
        [TestCase("**/CVS/*", "org/apache/CVS/Entries", true)]
        [TestCase("**/CVS/*", "org/apache/jakarta/tools/ant/CVS/Entries", true)]
        [TestCase("**/CVS/*", " org/apache/CVS/foo/bar/Entries", false)]
        // Jakarta examples
        [TestCase("org/apache/jakarta/**", "org/apache/jakarta/tools/ant/docs/index.html", true)]
        [TestCase("org/apache/jakarta/**", "org/apache/jakarta/test.xml", true)]
        [TestCase("org/apache/jakarta/**", "org/apache/xyz.java", false)]
        // CVS with prefix path examples
        [TestCase("org/apache/**/CVS/*", "org/apache/CVS/Entries", true)]
        [TestCase("org/apache/**/CVS/*", "org/apache/jakarta/tools/ant/CVS/Entries", true)]
        [TestCase("org/apache/**/CVS/*", "org/apache/CVS/foo/bar/Entries", false)]
        // Test examples
        [TestCase("**/test/**", "test", true)]
        [TestCase("**/test/**", "test.png", true)]
        [TestCase("**/test/**/file.png", "testfile.png", false)]
        [TestCase("mypackage/test/", "mypackage/test/file.png", true)]
        public void Matcher_WhenGivenExamplesFromApacheDocs_ReturnsExpected(
            string pattern,
            string input,
            bool shouldMatch)
        {
            var matcher = new Ant(pattern);
            var result = matcher.IsMatch(input);

            Assert.That(matcher.ToString(), Is.EqualTo(pattern));
            Assert.That(result, Is.EqualTo(shouldMatch));
        }
    }
}
