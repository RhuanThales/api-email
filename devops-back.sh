#!/bin/bash
nome_projeto=`dirs|awk -F"/" '{print $NF}'`"-image"

echo 'Devops - '$nome_projeto

# removendo a publish_output
echo "Removendo publish_output atual..."
if [ -d 'publish_output' -a -r 'publish_output' ]; then
        rm -R publish_output
fi

echo "Gerando novo build..."
#publicando publish_output
dotnet publish -c Release -o publish_output

echo "Removendo Migrations..."
# removendo a Migrations
if [ -d 'Migrations' -a -r 'Migrations' ]; then
        rm -R Migrations
fi

#nome_projeto='sistema-usuarios-image'

#docker images | grep 'sistema-diarias-image' | awk '{print $3} 
img=` docker images | grep '172.21.63.23:5000/'$nome_projeto | awk '{print $1}'`
echo $img

if [ -z $img ]; then
	echo "Não há imagem referente no docker..."
else
	echo "Removendo imagem existente no docker..."
	docker rmi $img
fi

echo "Gerando novo conteiner..."
#build novo conteiner
docker build -t $nome_projeto .

echo "Adicionar tag para o docker registry local..."
#adicionar tag do docker registry local
docker tag $nome_projeto 172.21.63.23:5000/$nome_projeto

echo "Enviando conteiner para o docker registry local..."
docker push 172.21.63.23:5000/$nome_projeto
