using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteMvc5.Models;
using TesteMvc5.Data;
using System.Web.UI.MobileControls;
using System.Data.Entity;

namespace TesteMvc5.Controllers
{
    public class AlunoController : Controller
    {
        //declarando um context
        private readonly AppDbContext context = new AppDbContext();
       
        [HttpGet]
        [Route(template: "novo-aluno")]
        public ActionResult NovoAluno()
        {           
            return View();
        }

        [HttpPost]
        [Route(template: "novo-aluno")]
        public ActionResult NovoAluno(Aluno aluno)
        {
            
            if (!ModelState.IsValid) return View(aluno);
            //Aqui trabalharimos alguma regra de negócio + salvar no banco 
            //passando a data do aluno
            aluno.DataMatricula = DateTime.Now;
            context.Alunos.Add(aluno);
            //salvando no banco
            context.SaveChanges();
            return View(aluno);
        }

        [HttpGet]
        [Route(template: "obter-alunos")]
        public ActionResult ObterAlunos()
        {
            //convertendo o resultado eum
            //uma lista de alunos
            var alunos = new List<Aluno>();
            alunos = context.Alunos.ToList();
            
            //pegando o primeiro aluno da coleção
            return View("NovoAluno", model: alunos.FirstOrDefault());
        }

        [HttpGet]
        [Route(template: "editar-aluno")]
        public ActionResult EditarAluno()
        {
            //fazendo uma pesquesa retornando uma lista
            var aluno = context.Alunos.FirstOrDefault(x => x.Id == 1);
            
            //exemplo pegando registro
            aluno.Nome = "Fabiana Lima";

            //Entrtando com um objeto já existente
            var alteraregistro = context.Entry(aluno);
            context.Set<Aluno>().Attach(aluno);
            //Atualizando um registro exitente
            alteraregistro.State = EntityState.Modified;
            //salvando
            context.SaveChanges();

            //recuperando o objeto altoredo
            var aulunonovo = context.Alunos.Find(aluno.Id);

            //Pasando o aluno modificado
            return View("NovoAluno", aulunonovo);
        }

        [HttpGet]
        [Route(template: "excluir-aluno")]
        public ActionResult ExcluirAluno()
        {
            // Buscando o aluno modificado pela edição
            var aluno = context.Alunos.FirstOrDefault(x => x.Nome == "Fabiana Lima");
            // no caso vamos recuperar o objeto e remover o aluno
            context.Alunos.Remove(aluno);
            //salvando
            context.SaveChanges();

            //Buscando o aluno da lista caso exita
            List<Aluno> alunos = new List<Aluno>();
            alunos = context.Alunos.ToList();

            return View("NovoAluno", alunos.FirstOrDefault());
        }

    }
}