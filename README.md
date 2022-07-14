# Hobby Stacks - ASP.NET Core / React

[![StackShare](http://img.shields.io/badge/tech-stack-0690fa.svg?style=flat)](https://stackshare.io/PhiltasticGuy/hobbystacks)

## Why create *Hobby Stacks*?

Our goal is to define a technology stack for hobby projects with **minimal costs**. We also want to offer real-world examples that go beyond canned tutorials showcasing isolated technologies or features.

We decided to gear this tech stack towards **commercial projects** which meant opting for a private container registry and code repository.

This example application uses **.NET Core (ASP.NET Core)**, **React** and **PostgreSQL**.

| Features                         | Tools                                  | Alternatives       | Costs |
| -------------------------------- | -------------------------------------- | ------------------ | ----- |
| Hosting                          | [DigitalOcean](https://www.digitalocean.com/pricing/#Compute) | [Vultur](https://www.vultr.com/products/cloud-compute/#pricing) | **5$**    |
| Container Registry (**Private**) | [Azure Container Registry](https://azure.microsoft.com/en-ca/services/container-registry/) | [GitLab Container Registry](https://docs.gitlab.com/ee/user/project/container_registry.html)* | **5$** / Free |
| Repositories (**Private**)       | [Azure DevOps](https://azure.microsoft.com/en-ca/services/devops/git-repos/) | GitLab, GitHub      | *Free*  |
| CI/CD Pipelines                  | [Azure Pipelines](https://azure.microsoft.com/en-ca/services/devops/pipelines/) | GitLab, CircleCI, [Travis CI](https://travis-ci.com/plans/) | *Free*  |
| Web Server (Reverse Proxy)       | Nginx                                  |                    | *Free*  |
| Web Performance & Security       | [Cloudflare](https://www.cloudflare.com/plans/#compare-features) | | *Free*  |
| SSL Certificates                 | [Let's Encrypt](https://letsencrypt.org/about/) + [Certbot](https://certbot.eff.org/about/) | | *Free*  |
| Multi-Container Tool             | Docker Compose                         |                    | *Free*  |
| IDE                              | [Visual Studio Community](https://visualstudio.microsoft.com/vs/community/) | [Visual Studio Code](https://code.visualstudio.com/) | *Free*  |
| Front-end + UI                   | ASP.NET Core - MVC + React             |                    | *Free*  |
| APIs                             | ASP.NET Core - Web API                 |                    | *Free*  |
| Database                         | PostgreSQL                             |                    | *Free*  |

*\* GitLab Container Registry is only available for projects hosted on GitLab.*
