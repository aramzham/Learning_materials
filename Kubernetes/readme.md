Kubernetes is an open source container orchestrator.

why k8s? because there are 8 characters between k and s in "kubernetes"

a node is a machine, could be a virtual machine, could be a real physical machine

a cluster is a group of nodes, a group of machines

think about pod as a container

a namespace is a logical grouping of things (like environments)

curl.exe -Lo kind-windows-amd64.exe https://kind.sigs.k8s.io/dl/v0.26.0/kind-windows-amd64
Move-Item .\kind-windows-amd64.exe c:\some-dir-in-your-PATH\kind.exe
this is to install kind

kind create cluster --name k8s-course - create a cluster with kind

kubectl get nodes - get nodes :)

kubectl get pods --all-namespaces - get all pods

if kubectl is not installed with docker desktop, install it with `https://kubernetes.io/docs/tasks/tools/`

run "notepad $profile" and add a line "set-alias k kubectl" not to type kubectl all the time

k get ns - get namespaces

How to install k9s?
- go to `https://github.com/derailed/k9s/releases`
- download `k9s_Windows_amd64.zip` from latest release
- add k9s to path
  - Press Win + R, type sysdm.cpl, and press Enter.
  - Go to the Advanced tab and click Environment Variables.
  - Under System variables, find the Path variable and click Edit.
  - Click New and add the path to the directory containing k9s.exe (e.g., C:\Program Files\k9s).

k apply -f {path_to_file} - apply a yaml file

k rollout undo deployment/{deployment_name} - if you have messed up with your deployment you can rollback with this command