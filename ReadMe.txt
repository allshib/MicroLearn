создать кубернетес: kubectl apply -f platforms-depl.yaml 
получить деплоймент: kubectl get deployments
ѕолучить капсулы: kubectl get pods
удалить развертывание: kubectl delete deployment platforms-depl
перезапустить развертывание: kubectl rollout restart deployment platforms-depl
