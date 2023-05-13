# FROM mcr.microsoft.com/dotnet/core/sdk:7.0 AS build

# ENV ASPNETCORE_URLS=http://+:80
# ENV DOTNET_RUNNING_IN_CONTAINER=true

# WORKDIR /build

# COPY ["src/Web/monolithic.Api/monolithic.Api.csproj", "src/monolithic.Api/"]
# COPY ["src/Core/Application/Monolithic.Core.Application/Monolithic.Core.Application.csproj", "src/Monolithic.Core.Application/"]
# COPY ["src/Core/Domain/Customer/Monolithic.Core.Domain.Customer/Monolithic.Core.Domain.Customer.csproj", "src/Monolithic.Core.Domain.Customer/"]
# COPY ["src/Core/Domain/Order/Monolithic.Core.Domain.Order/Monolithic.Core.Domain.Order.csproj", "src/Monolithic.Core.Domain.Order/"]
# COPY ["src/Core/Domain/Product/Monolithic.Core.Domain.Product/Monolithic.Core.Domain.Product.csproj", "src/Monolithic.Core.Domain.Product/"]
# COPY ["src/Core/Infrastructure/Monolithic.Core.Infrastructure/Monolithic.Core.Infrastructure.csproj", "src/Monolithic.Core.Infrastructure/"]
# COPY ["src/Core/SharedKernel/Monolithic.Core.SharedKernel/Monolithic.Core.SharedKernel.csproj", "src/Monolithic.Core.SharedKernel/"]
# COPY ["src/IoC/monolithic.Api/monolithic.IoC.csproj", "src/monolithic.IoC/"]

# RUN dotnet restore "src/monolithic.Api/monolithic.Api.csproj"
# COPY . .
# WORKDIR /build/src/monolithic.Api
# esse
# FROM build AS publish
# RUN dotnet publish "monolithic.Api.csproj" -c Release -o /publish

# FROM mcr.microsoft.com/dotnet/core/aspnet:7.0 AS app

# # Install the agent

# # Download and install the Tracer
# # RUN mkdir -p /opt/datadog \
# #     && mkdir -p /var/log/datadog \
# #     && TRACER_VERSION=$(curl -s https://api.github.com/repos/DataDog/dd-trace-dotnet/releases/latest | grep tag_name | cut -d '"' -f 4 | cut -c2-) \
# #     && curl -LO https://github.com/DataDog/dd-trace-dotnet/releases/download/v${TRACER_VERSION}/datadog-dotnet-apm_${TRACER_VERSION}_amd64.deb \
# #     && dpkg -i ./datadog-dotnet-apm_${TRACER_VERSION}_amd64.deb \
# #     && rm ./datadog-dotnet-apm_${TRACER_VERSION}_amd64.deb

# # # Enable the tracer
# # ENV CORECLR_ENABLE_PROFILING=1
# # ENV CORECLR_PROFILER={846F5F1C-F9AE-4B07-969E-05C26BC060D8}
# # ENV CORECLR_PROFILER_PATH=/opt/datadog/Datadog.Trace.ClrProfiler.Native.so
# # ENV DD_DOTNET_TRACER_HOME=/opt/datadog
# # ENV DD_INTEGRATIONS=/opt/datadog/integrations.json
# # ENV DD_ENV=prd
# # ENV DD_SERVICE=monolithicprd
# # ENV DD_AGENT_HOST=apmddgagent.eastus.azurecontainer.io
# # ENV DD_LOGS_INJECTION=true


# WORKDIR /app
# COPY --from=publish /publish .

# RUN sed -i 's|DEFAULT@SECLEVEL=2|DEFAULT@SECLEVEL=1|g' /etc/ssl/openssl.cnf

# ENTRYPOINT "dotnet" "monolithic.Api.dll" --urls="http://0.0.0.0:${PORT:-80}"