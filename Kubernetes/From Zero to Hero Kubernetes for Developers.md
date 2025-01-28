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