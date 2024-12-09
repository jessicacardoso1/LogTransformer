# 📝 Log Transformer

Projeto para transformar logs em formatos pré-definidos. Este projeto foi desenvolvido utilizando .NET Core e segue os princípios de código limpo, com modularidade e boas práticas aplicadas.

---

## 📚 Funcionalidades

- **Transformação de Logs**: Processa logs em formato bruto e os converte em um formato legível e padronizado.
- **Entrada via URL ou ID**: Aceita logs fornecidos via URL ou através de um ID previamente armazenado.
- **Armazenamento de Transformações**: Salva logs transformados no sistema de arquivos e no banco de dados.
- **Geração de Arquivo ou Texto**: Retorna o log transformado como um arquivo ou texto formatado.

---

## 🛠️ Tecnologias

- **Linguagem**: C#
- **Framework**: .NET Core
- **Padrões**: Clean Code e Clean Architecture
- **Banco de Dados**: SQL Server
- **Dependências**:
  - **MediatR**: Para a orquestração de comandos e handlers.
  - **Newtonsoft.Json**: Para manipulação de JSON.
  - **Injeção de Dependência**: Seguindo os princípios de SOLID.

---

## 🧩 Como Usar

### Via ID do Log

Envie um comando com o `LogId` preenchido:

{
  "logId": 1,
  "outputFormat": "file"
}

### Via URL do Log

Envie um comando com o `LogId` preenchido:

{
  "LogUrl": "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt",
  "OutputFormat": "content"
}

### Formato de Saída
O parâmetro outputFormat determina como o log transformado será retornado:

file: O log transformado é salvo como um arquivo, e o caminho do arquivo é retornado.

content: O log transformado é retornado como uma string de texto formatada.