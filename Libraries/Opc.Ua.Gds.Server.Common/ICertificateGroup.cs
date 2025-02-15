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

using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Opc.Ua.Security.Certificates;

namespace Opc.Ua.Gds.Server
{
    public class X509Certificate2KeyPair
    {
        public X509Certificate2 Certificate { get; }
        public string PrivateKeyFormat { get; }
        public byte[] PrivateKey { get; }

        public X509Certificate2KeyPair(X509Certificate2 certificate, string privateKeyFormat, byte[] privateKey)
        {
            if (certificate.HasPrivateKey)
            {
                certificate = X509CertificateLoader.LoadCertificate(certificate.RawData);
            }
            Certificate = certificate;
            PrivateKeyFormat = privateKeyFormat;
            PrivateKey = privateKey;
        }
    };

    /// <summary>
    /// An abstract interface to the certificate provider
    /// </summary>
    public interface ICertificateGroup
    {
        NodeId Id { get; set; }
        NodeIdCollection CertificateTypes { get; set; }
        ConcurrentDictionary<NodeId, X509Certificate2> Certificates { get; }
        CertificateGroupConfiguration Configuration { get; }
        CertificateStoreIdentifier AuthoritiesStore { get; }
        CertificateStoreIdentifier IssuerCertificatesStore { get; }
        TrustListState DefaultTrustList { get; set; }
        bool UpdateRequired { get; set; }

        ICertificateGroup Create(
            string authoritiesStorePath,
            CertificateGroupConfiguration certificateGroupConfiguration,
            [Optional] string issuerCertificatesStorePath);

        Task Init();

        Task<X509Certificate2> CreateCACertificateAsync(
            string subjectName,
            NodeId certificateType
            );

        Task<X509CRL> RevokeCertificateAsync(
            X509Certificate2 certificate
            );

        Task VerifySigningRequestAsync(
            ApplicationRecordDataType application,
            byte[] certificateRequest
            );

        Task<X509Certificate2> SigningRequestAsync(
            ApplicationRecordDataType application,
            NodeId certificateType,
            string[] domainNames,
            byte[] certificateRequest
            );

        Task<X509Certificate2KeyPair> NewKeyPairRequestAsync(
            ApplicationRecordDataType application,
            NodeId certificateType,
            string subjectName,
            string[] domainNames,
            string privateKeyFormat,
            string privateKeyPassword
            );
    }
}
