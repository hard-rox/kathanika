# Deployment Guide

## Overview

Kathanika uses a comprehensive CI/CD pipeline built on GitHub Actions for automated testing, building, and deployment.

## Pipeline Stages

### 1. Quality Gate (Automatic)
- **Triggers:** Every push to `main` branch or version tags (`v*.*.*`)
- **Steps:**
  - Lints .NET and Angular code
  - Builds backend and frontend
  - Runs unit tests with coverage
  - Performs SonarCloud analysis
  - Waits for Quality Gate to pass

### 2. Build & Push Docker Image (Automatic)
- **Multi-platform builds:** linux/amd64, linux/arm64
- **Image tagging:**
  - `main` → `latest` + `main-{short-sha}`
  - `v1.2.3` → `1.2.3`, `1.2`, `1`, `latest`
- **Artifacts:**
  - SBOM (Software Bill of Materials)
  - Build provenance attestation
- **Registry:** Docker Hub (docker.io)

### 3. Security Scanning (Automatic)
- **Tool:** Trivy vulnerability scanner
- **Severities:** CRITICAL and HIGH
- **Actions:**
  - Uploads results to GitHub Security tab
  - Fails pipeline on critical vulnerabilities
  - Ignores unfixed vulnerabilities

### 4. Deployment Stages

#### Development (Automatic)
- **Trigger:** Every push to `main` branch
- **Auto-deploys** after quality gate and security scan pass
- **URL:** [https://dev.kathanika.example.com](https://dev.kathanika.example.com)

#### Staging (Manual Approval)
- **Trigger:** Version tags (`v*.*.*`)
- **Requires:** Manual approval in GitHub Environments
- **Includes:** Smoke tests
- **URL:** [https://staging.kathanika.example.com](https://staging.kathanika.example.com)

#### Production (Manual Approval)
- **Trigger:** After successful staging deployment
- **Requires:** Manual approval in GitHub Environments
- **Includes:** Health checks and GitHub Release creation
- **URL:** [https://kathanika.example.com](https://kathanika.example.com)

## Prerequisites

### 1. GitHub Secrets
Configure these in repository settings → Secrets and variables → Actions:

```bash
SONAR_TOKEN           # SonarCloud authentication token
```

### 2. GitHub Environments
Create these environments with protection rules:

**development:**
- No approval required
- Deploy from: `main` branch only

**staging:**
- Required reviewers: 1
- Deploy from: `v*.*.*` tags only

**production:**
- Required reviewers: 2+
- Wait timer: 60 minutes
- Deploy from: `v*.*.*` tags only

**rollback:**
- Required reviewers: 1
- Used only for emergency rollbacks

### 3. Container Registry Access
The pipeline uses Docker Hub (docker.io) for container images.

Images are pushed to: `hard-rox/kathanika`

**Required GitHub Secrets:**
```bash
DOCKERHUB_USERNAME    # Docker Hub username
DOCKERHUB_TOKEN       # Docker Hub access token
```

## Deployment Methods

### Docker Compose (Recommended for Testing)

```bash
# Clone repository
git clone https://github.com/hard-rox/kathanika.git
cd kathanika

# Copy environment template
cp .env.example .env
# Edit .env with your settings

# Start all services
docker-compose up -d

# View logs
docker-compose logs -f kathanika

# Stop services
docker-compose down
```

### Docker Run (Single Container)

```bash
docker run -d \
  --name kathanika \
  -p 8080:8080 \
  -e ConnectionStrings__mongodb="mongodb://user:pass@mongo:27017/kathanika" \
  -e ConnectionStrings__redis="redis:6379" \
  -e ConnectionStrings__azureBlobStorage="your-blob-connection" \
  -e ASPNETCORE_ENVIRONMENT=Production \
  hard-rox/kathanika:latest
```

### Kubernetes

```bash
# Pull image
kubectl create secret docker-registry dockerhub-secret \
  --docker-server=docker.io \
  --docker-username=YOUR_DOCKERHUB_USERNAME \
  --docker-password=YOUR_DOCKERHUB_TOKEN

# Create secrets for connection strings
kubectl create secret generic kathanika-secrets \
  --from-literal=mongodb="mongodb://..." \
  --from-literal=redis="redis:6379" \
  --from-literal=azureBlob="..."

# Apply manifests
kubectl apply -f k8s/

# Check deployment
kubectl get pods -l app=kathanika
kubectl logs -l app=kathanika -f
```

### Helm Chart (Coming Soon)

```bash
helm install kathanika ./charts/kathanika \
  --set image.tag=1.0.0 \
  --set mongodb.connectionString="..." \
  --set redis.connectionString="..." \
  --set azureBlob.connectionString="..."
```

## Version Management

### Semantic Versioning
Follow [Semantic Versioning 2.0.0](https://semver.org/):
- **Major (X.0.0):** Breaking changes
- **Minor (0.X.0):** New features, backward-compatible
- **Patch (0.0.X):** Bug fixes

### Creating a Release

```bash
# Ensure main is up to date
git checkout main
git pull

# Create and push version tag
git tag -a v1.2.3 -m "Release version 1.2.3"
git push origin v1.2.3
```

This will:
1. Trigger the CD pipeline
2. Run quality gates
3. Build multi-platform Docker images
4. Tag images with version numbers
5. Deploy to staging (after approval)
6. Deploy to production (after approval)
7. Create GitHub Release with notes

## Monitoring & Rollback

### View Deployment Status
- GitHub Actions tab shows pipeline progress
- Each job has detailed logs
- Security scan results in Security tab

### Rollback Strategy

#### Automatic Rollback
If deployment to any environment fails, the rollback job can be triggered manually.

#### Manual Rollback

**Docker:**
```bash
docker pull hard-rox/kathanika:v1.1.0  # Previous version
docker stop kathanika && docker rm kathanika
docker run -d --name kathanika ... hard-rox/kathanika:v1.1.0
```

**Kubernetes:**
```bash
kubectl rollout undo deployment/kathanika
# OR
kubectl set image deployment/kathanika kathanika=hard-rox/kathanika:v1.1.0
```

**Helm:**
```bash
helm rollback kathanika
# OR
helm rollback kathanika 3  # Specific revision
```

## Environment Variables Reference

### Required
```bash
ConnectionStrings__mongodb          # MongoDB connection string
ConnectionStrings__redis            # Redis connection string
ConnectionStrings__azureBlobStorage # Azure Blob Storage connection string
```

### Optional
```bash
ASPNETCORE_ENVIRONMENT              # Development/Staging/Production (default: Production)
ApplicationOptions__UploadPath      # Upload directory path (default: /app/uploads)
Serilog__MinimumLevel__Default      # Log level (default: Information)
OTEL_EXPORTER_OTLP_ENDPOINT        # OpenTelemetry endpoint (optional)
```

## Health Checks

### Application Health Endpoint
```bash
curl http://localhost:8080/health
```

Expected response: `200 OK`

### Docker Health Check
```bash
docker inspect --format='{{.State.Health.Status}}' kathanika
```

Expected: `healthy`

## Troubleshooting

### Build Failures
1. Check GitHub Actions logs for specific error
2. Run locally: `docker build -t kathanika:test .`
3. Verify all tests pass: `dotnet test && npm test`

### Deployment Failures
1. Check environment secrets are set correctly
2. Verify connection strings are valid
3. Check resource availability (memory, CPU, disk)
4. Review application logs: `docker logs kathanika`

### Quality Gate Failures
1. Review SonarCloud dashboard
2. Fix code smells and security hotspots
3. Ensure test coverage meets minimum threshold
4. Re-run pipeline after fixes

## Best Practices

1. **Never commit secrets** - Use environment variables or secrets management
2. **Always test locally** before pushing to main
3. **Use feature branches** and pull requests
4. **Tag releases** following semantic versioning
5. **Monitor deployments** and set up alerts
6. **Keep dependencies updated** (Dependabot enabled)
7. **Review security scans** before production deployment
8. **Document breaking changes** in release notes

## Support

- **Issues:** [https://github.com/hard-rox/kathanika/issues](https://github.com/hard-rox/kathanika/issues)
- **Discussions:** [https://github.com/hard-rox/kathanika/discussions](https://github.com/hard-rox/kathanika/discussions)
- **Documentation:** /docs/
