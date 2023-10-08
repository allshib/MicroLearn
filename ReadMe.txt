создать кубернетес: kubectl apply -f platforms-depl.yaml 
получить деплоймент: kubectl get deployments
ѕолучить капсулы: kubectl get pods
удалить развертывание: kubectl delete deployment platforms-depl
перезапустить развертывание: kubectl rollout restart deployment platforms-depl
создание секретов: kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"
—оздание миграции: dotnet ef migrations add initialmigration