global using ConsoleApp.Extensions;
global using Domain.Common;
global using Domain.Exceptions;
global using Infrastructure.Filters;
global using Infrastructure.Handler;
global using Application.Services;
global using Application.Interfaces;
global using Application;

global using System.Net;

global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Options;

global using RestEase.HttpClientFactory;
global using Polly;
global using Polly.Extensions.Http;
