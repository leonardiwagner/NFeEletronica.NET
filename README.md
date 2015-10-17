<h1>
  <img src="http://www.piaui.pi.gov.br/images/albuns/album_121/2c8b41305f_media.jpg" width="48">
  NFeEletronica.NET
  <a href="https://travis-ci.org/leonardiwagner/NFeEletronica.NET" target="_blank">
    <img title="Build Status Images" src="https://travis-ci.org/leonardiwagner/NFeEletronica.NET.svg">
  </a>
</h1>
E-invoicing issuer for the Brazilian government / Emissor de nota fiscal eletrônica

Install via NuGet! : `PM> Install-Package NFeEletronica`

This project is intended for use only in Brazil, so project is all written in Portuguese, but feel free to ask questions in english too.

Thanks to TeamCity for the free build server to support this project! [They are awesome :)](http://teamcity.codebetter.com/)

### Como Usar?
1. Adicione via NuGet o pacote [NFeEletronica](https://www.nuget.org/packages/NFeEletronica/) no seu projeto.
 -  `PM> Install-Package NFeEletronica` no Package Manager Console
 -  Ou procure por "NFeEletronica" no gerenciador de pacotes do NuGet e instale!
2. Veja os [exemplos de uso](https://github.com/leonardiwagner/WallegNFe/wiki) do WallegNFe:
 - [Criando uma nota fiscal e (opcionalmente) salvando em XML](https://github.com/leonardiwagner/WallegNFe/wiki/Criando-uma-nota-fiscal-eletr%C3%B4nica-e-salvando-em-XML)
 - [Transmitindo uma nota fiscal para a SEFAZ](https://github.com/leonardiwagner/WallegNFe/wiki/Transmitindo-uma-nota-para-a-SEFAZ)
 - [Cancelando uma nota fiscal](https://github.com/leonardiwagner/WallegNFe/wiki/Cancelando-uma-nota)
 - [Inutilização de notas fiscais](https://github.com/leonardiwagner/WallegNFe/wiki/Inutilizando-notas)
 
### Esse projeto te ajudou?
Espero que sim, então de uma estrelinha no projeto para sinalizar que ele pode ser útil para outras pessoas também.

Ao contrário da maioria das bibliotecas de NFe para .NET, esse é de graça e tem o código totalmente aberto para servir de uso para seu projeto também. Fiz ele utilizando meu tempo livre e não recebi nada por isso, então peço que caso tenha alguma dúvida ou sugestão, favor reportar via [issues do projeto](https://github.com/leonardiwagner/NFeEletronica.NET/issues) ao invés de me procurar por e-mail, obrigado! Pull requests também serão bem vindos.
