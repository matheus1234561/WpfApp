# WpfApp

## Como rodar?

1. Clone o reposit�rio na sua m�quina.
2. Por padr�o, o projeto j� possui os arquivos XML onde ser�o salvos os dados necess�rios.
3. Os pacotes necess�rios que foram instalados durante o desenvolvimento s�o:
   - `CommonServiceLocator`
   - `MvvmLightLibs`
   - `System.ComponentModel.Annotations`
4. Depois, basta clicar em **Start** no Visual Studio para abrir as janelas.

## Como funciona?

O projeto foi desenvolvido em **.NET Framework 4.6** utilizando **WPF**, e usa arquivos XML para armazenar os dados salvos.

Ele possui tr�s janelas:
- **Person Window**
- **Product Window**
- **Order Window**

Cada janela tem fun��es de:
- Excluir
- Adicionar
- Salvar no arquivo XML

Al�m disso, o projeto inclui valida��es para garantir que os dados sejam inseridos corretamente.

## Observa��es

Na hora de adicionar pessoas e produtos, se atentar em dar um "TAB" para a interface cria uma nova row para adicionar o pr�ximo.