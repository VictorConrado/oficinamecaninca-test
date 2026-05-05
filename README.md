# 🔧 Oficina Mecânica API

API REST desenvolvida em **.NET 9** para gerenciamento de orçamentos de oficina mecânica.

---

## 🚀 Tecnologias

- .NET 9 / ASP.NET Core
- xUnit + Moq (testes)
- Swagger / OpenAPI

---

## 📁 Estrutura

```
OficinaMecanica/
├── Controllers/        # Camada HTTP
├── DTOs/               # Objetos de entrada e saída
├── Models/             # Entidades de domínio
├── Repositories/       # Acesso a dados
└── Services/           # Regras de negócio

OficinaMecanica.Tests/
├── Controllers/
├── Repositories/
└── Services/
```

---

## 📋 Endpoint

### `POST /api/orcamento`

Cadastra um novo orçamento.

**Request**
```json
{
  "clienteId": 10,
  "veiculoId": 25,
  "itens": [
    { "descricao": "Troca de óleo", "quantidade": 1, "valorUnitario": 120.00 },
    { "descricao": "Filtro de óleo", "quantidade": 1, "valorUnitario": 45.00 }
  ]
}
```

**Response `201 Created`**
```json
{
  "id": 1,
  "clienteId": 10,
  "veiculoId": 25,
  "itens": [
    { "descricao": "Troca de óleo", "quantidade": 1, "valorUnitario": 120.00, "subtotal": 120.00 },
    { "descricao": "Filtro de óleo", "quantidade": 1, "valorUnitario": 45.00, "subtotal": 45.00 }
  ],
  "total": 165.00,
  "criadoEm": "2026-05-05T20:00:00Z"
}
```

**Validações — `400 Bad Request`**
| Regra | Mensagem |
|---|---|
| `clienteId` ausente | clienteId é obrigatório |
| `veiculoId` ausente | veiculoId é obrigatório |
| Nenhum item | O orçamento deve ter pelo menos 1 item |
| Quantidade ≤ 0 | A quantidade deve ser maior que zero |
| Valor unitário ≤ 0 | O valor unitário deve ser maior que zero |

---

## ✅ Testes

```bash
dotnet test
```

```
total: 12 | passou: 12 | falhou: 0
```

Cobertura por camada:

| Camada | Testes | Estratégia |
|---|---|---|
| Repository | 4 | Instância real |
| Service | 5 | Mock do Repository |
| Controller | 3 | Mock do Service |

---

## ▶️ Como rodar

```bash
git clone https://github.com/seu-usuario/oficina-mecanica-api
cd OficinaMecanica
dotnet run
```

Acesse o Swagger em: `https://localhost:{porta}/swagger`
