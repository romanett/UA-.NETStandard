/* Copyright (c) 1996-2022 The OPC Foundation. All rights reserved.
   The source code in this file is covered under a dual-license scenario:
     - RCL: for OPC Foundation Corporate Members in good-standing
     - GPL V2: everybody else
   RCL license terms accompanied with this source code. See http://opcfoundation.org/License/RCL/1.00/
   GNU General Public License as published by the Free Software Foundation;
   version 2 of the License are accompanied with this source code. See http://opcfoundation.org/License/GPLv2
   This source code is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
*/

#nullable enable

using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Opc.Ua
{
    /// <summary>
    /// An abstract interface to the certificate validator.
    /// </summary>
    public interface ICertificateValidator
    {
        /// <summary>
        /// Validates a certificate.
        /// </summary>
        void Validate(X509Certificate2 certificate);

        /// <summary>
        /// Validates a certificate chain.
        /// </summary>
        void Validate(X509Certificate2Collection certificateChain);

        /// <summary>
        /// Validates a certificate.
        /// </summary>
        Task ValidateAsync(X509Certificate2 certificate, CancellationToken ct);

        /// <summary>
        /// Validates a certificate chain.
        /// </summary>
        Task ValidateAsync(X509Certificate2Collection certificateChain, CancellationToken ct);
    }
}
