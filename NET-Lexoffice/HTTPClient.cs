﻿// MIT License
// 
// Copyright (c) 2020 Nils Kleinert
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#region Usings

using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;

#endregion

namespace NET_Lexoffice
{
    public class HTTPClient
    {
        private readonly string _apiKey;

        public HTTPClient(string apiKey)
        {
            _apiKey = apiKey;
        }


        public async Task<string> Send(Method method, string path, params Parameter[] parameters)
        {
            RestClient client = new RestClient($"https://api.lexoffice.io/v1/{path}")
            {
                Timeout = -1
            };

            RestRequest request = new RestRequest(method);

            request.AddHeader("Authorization", $"Bearer {_apiKey}");
            request.AddHeader("Accept", "application/json");

            if (parameters != null)
                foreach (Parameter parameter in parameters)
                    request.AddParameter(parameter);

            IRestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return response.Content;
            throw new AuthenticationException("Invalid API Key");

        }
    }
}