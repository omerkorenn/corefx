// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xunit;

namespace System.Globalization.Tests
{
    public class NumberFormatInfoPercentNegativePattern
    {
        public static IEnumerable<object[]> PercentNegativePattern_TestData()
        {
            yield return new object[] { new CultureInfo("en-US").NumberFormat, 1 };
            yield return new object[] { new CultureInfo("en-MY").NumberFormat, 1 };
            yield return new object[] { new CultureInfo("tr").NumberFormat, 2 };
        }

        /// <summary>
        /// Not testing for Windows as the culture data can change
        /// https://blogs.msdn.microsoft.com/shawnste/2005/04/05/culture-data-shouldnt-be-considered-stable-except-for-invariant/
        /// In the CultureInfoAll test class we are testing the expected behavior 
        /// for Windows by enumerating all locales on the system and then test them. 
        /// </summary>
        [Theory]
        [PlatformSpecific(TestPlatforms.AnyUnix)]
        [MemberData(nameof(PercentNegativePattern_TestData))]
        public void PercentNegativePattern_Get(NumberFormatInfo format, int expected)
        {
            Assert.Equal(expected, format.PercentNegativePattern);
        }

        [Fact]
        public void PercentNegativePattern_Invariant_Get()
        {
            Assert.Equal(0, NumberFormatInfo.InvariantInfo.PercentNegativePattern);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(11)]
        public void PercentNegativePattern_Set(int newPercentNegativePattern)
        {
            NumberFormatInfo format = new NumberFormatInfo();
            format.PercentNegativePattern = newPercentNegativePattern;
            Assert.Equal(newPercentNegativePattern, format.PercentNegativePattern);
        }

        [Fact]
        public void PercentNegativePattern_Set_Invalid()
        {
            AssertExtensions.Throws<ArgumentOutOfRangeException>("PercentNegativePattern", () => new NumberFormatInfo().PercentNegativePattern = -1);
            AssertExtensions.Throws<ArgumentOutOfRangeException>("PercentNegativePattern", () => new NumberFormatInfo().PercentNegativePattern = 12);

            Assert.Throws<InvalidOperationException>(() => NumberFormatInfo.InvariantInfo.PercentNegativePattern = 1);
        }
    }
}
