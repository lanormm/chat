# Servidor

Executar "server.exe".

# Cliente

Executar "client.exe"

## Básico
Chat com funcionalidade básica de conversas criptografas com o RSA + DES. 
Socket não bloqueante do lado do servidor e cliente. 
Esquema de criptografia híbrida. 

## RSA
RSACryptoServiceProvider - Classe que provê abstração ao algoritmo RSA 

## Exportando a chave pública privada
Como Bytes: RSACryptoServiceProvider.ExportCspBlob() 
Como Objeto: RSACryptoServiceProvider.ExportRSAParameters() 

## Importando a chave pública e privada do RSA
Como Bytes: RSACryptoServiceProvider.ImportCspBlob() 
Como Objeto: RSACryptoServiceProvider.ImportRSAParameters() 

## Criptrafar
RSACryptoServiceProvider.Encrypt() 

## DES
DESCryptoServiceProvider - Classe que provê abstração ao algoritmo DES 

## Exportando a Chave e o vetor de inicialização
DESCryptoServiceProvider.Key -- chave 
DESCryptoServiceProvider.IV -- vetor 

## Encriptar com o DES
Utilizar a classe CryptoStream como helper. Criar um encryptor com DESCryptoServiceProvider passando a chave 
e o IV, em modo CryptoStreamMode.Write.  
## Decriptar
Mesmo processo porém criar um encryptor e setar modo CryptoStreamMode.Read 
