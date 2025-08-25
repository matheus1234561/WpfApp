# WpfApp

## Como rodar?

1. Clone o repositório na sua máquina.
2. Por padrão, o projeto já possui os arquivos XML onde serão salvos os dados necessários.
3. Os pacotes necessários que foram instalados durante o desenvolvimento são:
   - `CommonServiceLocator`
   - `MvvmLightLibs`
   - `System.ComponentModel.Annotations`
4. Depois, basta clicar em **Start** no Visual Studio para abrir as janelas.

## Como funciona?

O projeto foi desenvolvido em **.NET Framework 4.6** utilizando **WPF**, e usa arquivos XML para armazenar os dados salvos.

Ele possui três janelas:
- **Person Window**
- **Product Window**
- **Order Window**

Cada janela tem funções de:
- Excluir
- Adicionar
- Salvar no arquivo XML

Além disso, o projeto inclui validações para garantir que os dados sejam inseridos corretamente.

## Observações

Na hora de adicionar pessoas e produtos, se atentar em dar um "TAB" para a interface cria uma nova row para adicionar o próximo.