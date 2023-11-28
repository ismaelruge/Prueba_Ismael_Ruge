const tab = document.getElementById('tbodyTab');

//Variables Crear
const form = document.getElementById('Crear');
const TipoDocumento = document.getElementById('TipoDocumento');
const NumDocumento = document.getElementById('NumDocumento');
const Nombres = document.getElementById('Nombres');
const Apellidos = document.getElementById('Apellidos');
const Email = document.getElementById('Email');
const Telefono = document.getElementById('Telefono');
const FNacimiento = document.getElementById('FNacimiento');
const Estado = document.getElementById('Estado');

const formEdit = document.getElementById('Editar');
const IdEdit = document.getElementById('IdEdit');
const TipoDocumentoEdit = document.getElementById('TipoDocumentoEdit');
const NumDocumentoEdit = document.getElementById('NumDocumentoEdit');
const NombresEdit = document.getElementById('NombresEdit');
const ApellidosEdit = document.getElementById('ApellidosEdit');
const EmailEdit = document.getElementById('EmailEdit');
const TelefonoEdit = document.getElementById('TelefonoEdit');
const FNacimientoEdit = document.getElementById('FNacimientoEdit');
const EstadoEdit = document.getElementById('EstadoEdit');

const url = '/api/Test';

CargarTabla();

async function CargarTabla() {
    tab.innerHTML = "";
    const d = await FetchGET(url);

    d.forEach((e) => {
        let opt = document.createElement('tr');

        opt.innerHTML = `<td>${e.id}</td>
                        <td>${e.nombres}</td>
                        <td>${e.apellidos}</td>
                        <td>${e.estadoAfiliacion}</td>
                        <td><button type="button" class="btn btn-success" onclick="Editar(${e.id})">Editar</button></td>
                        <td><button type="button" class="btn btn-danger" onclick="Eliminar(${e.id})">Eliminar</button></td>`;

        tab.appendChild(opt);
    });
}

async function Editar(Id) {
    const d = await FetchGET(`${url}/List?Id=${Id}`);
    let e = d[0];

    abrirModal('Modar_Edit');

    IdEdit.value = e.id;
    TipoDocumentoEdit.value = e.tipoDocumento
    NumDocumentoEdit.value = e.numeroDocumento;
    NombresEdit.value = e.nombres;
    ApellidosEdit.value = e.apellidos;
    EmailEdit.value = e.correoElectronico;
    TelefonoEdit.value = e.telefono
    FNacimientoEdit.value = e.fechaNacimiento.substring(0,10);
    EstadoEdit.checked = e.estadoAfiliacion;
}

async function Eliminar(Id) {
    await ProcedureApi(`${url}?Id=${Id}`, 'DELETE');
    await CargarTabla();
}

form.addEventListener('submit', async (e) => {
    e.preventDefault();

	let data = {
		tipoDocumento: TipoDocumento.value
		, numeroDocumento: NumDocumento.value
		, nombres: Nombres.value
		, apellidos: Apellidos.value
		, correoElectronico: Email.value
		, telefono: Telefono.value
		, fechaNacimiento: FNacimiento.value
		, estadoAfiliacion: Estado.checked
	}

    await ProcedureApi(url, 'POST', data);
    cerrarModal('Modar_Add');
    await CargarTabla();
});

formEdit.addEventListener('submit', async (e) => {
    e.preventDefault();

    let data = {
        id: IdEdit.value
        , tipoDocumento: TipoDocumentoEdit.value
        , numeroDocumento: NumDocumentoEdit.value
        , nombres: NombresEdit.value
        , apellidos: ApellidosEdit.value
        , correoElectronico: EmailEdit.value
        , telefono: TelefonoEdit.value
        , fechaNacimiento: FNacimientoEdit.value
        , estadoAfiliacion: EstadoEdit.checked
    }

    await ProcedureApi(url, 'PUT', data);
    cerrarModal('Modar_Edit');
    await CargarTabla();
});

async function ProcedureApi(url, method, data) {
    try {
        let options = {
            method: method,
            headers: {
                'Content-Type': 'application/json'
            }
        };
        if (data) {
            options.body = JSON.stringify(data);
        }
        const response = await fetch(url, options);
        if (!response.ok) {
            const errorText = await response.text();
            const errorMessage = errorText.match(/System\.Exception: (.*)/);
            alert(errorMessage[1]);
            throw new Error(errorMessage[1]);
        }
    } catch (error) {
        alert(error);
    }
}

async function FetchGET(url) {
    try {

        const response = await fetch(url);

        if (!response.ok) {
            const errorText = await response.text();
            const errorMessage = errorText.match(/System\.Exception: (.*)/);
            //alert(errorMessage[1]);
            throw new Error(errorMessage[1]);
        }

        const responseData = await response.json();
        if (!responseData.isSuccess) {
            //alert(responseData.message);
        }
        return responseData.data;
    } catch (error) {
        console.error(error);
    }
}

function abrirModal(modalId) {
    $('#' + modalId).modal('show');
}

function cerrarModal(modalId) {
    $('#' + modalId).modal('hide');
}