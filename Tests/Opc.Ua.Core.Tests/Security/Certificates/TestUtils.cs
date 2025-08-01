/* ========================================================================
 * Copyright (c) 2005-2020 The OPC Foundation, Inc. All rights reserved.
 *
 * OPC Foundation MIT License 1.00
 * 
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 *
 * The complete license agreement can be found here:
 * http://opcfoundation.org/License/MIT/1.00/
 * ======================================================================*/

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Opc.Ua.Core.Tests
{
    /// <summary>
    /// Common utilities for tests.
    /// </summary>
    public static class TestUtils
    {
        public static string[] EnumerateTestAssets(string searchPattern)
        {
            var assetsPath = Utils.GetAbsoluteDirectoryPath ("Assets", true, true, false);
            if (assetsPath != null)
            {
                return Directory.EnumerateFiles(assetsPath, searchPattern).ToArray();
            }
            return Array.Empty<string>();
        }

        /// <summary>
        /// A common method to clean up the test trust list.
        /// </summary>
        /// <param name="store"></param>
        /// <param name="dispose"></param>
        public static async Task CleanupTrustListAsync(ICertificateStore store, bool dispose = true)
        {
            if (store != null)
            {
                var certs = await store.Enumerate().ConfigureAwait(false);
                foreach (var cert in certs)
                {
                    await store.Delete(cert.Thumbprint).ConfigureAwait(false);
                }
                if (store.SupportsCRLs)
                {
                    var crls = await store.EnumerateCRLs().ConfigureAwait(false);
                    foreach (var crl in crls)
                    {
                        await store.DeleteCRL(crl).ConfigureAwait(false);
                    }
                }
                if (dispose)
                {
                    store.Dispose();
                }
            }
        }
    }
}
