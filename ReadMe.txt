������� ����������: kubectl apply -f platforms-depl.yaml 
�������� ����������: kubectl get deployments
�������� �������: kubectl get pods
������� �������������: kubectl delete deployment platforms-depl
������������� �������������: kubectl rollout restart deployment platforms-depl
�������� ��������: kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"
�������� ��������: dotnet ef migrations add initialmigration