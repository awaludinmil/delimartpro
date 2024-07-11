@extends('layouts.app')

@section('title', 'Form Tambah Kategori')

@section('contents')
<form action="{{ route('kategori.tambah.simpan') }}" method="post">
    @csrf
    <div class="row">
        <div class="col-12">
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Form Tambah Kategori</h6>
                </div>
                <div class="card-body">
                    <div class="form-group">
                        <label for="kd_kategori">Kode Kategori</label>
                        <input type="text" class="form-control" id="kd_kategori" name="kd_kategori">
                    </div>
                    <div class="form-group">
                        <label for="nama_kategori">Nama Kategori</label>
                        <input type="text" class="form-control" id="nama_kategori" name="nama_kategori">
                    </div>
                </div>
                <div class="card-footer">
                    <button type="submit" class="btn btn-primary">Simpan</button>
                    <a href="{{ route('kategori') }}" class="btn btn-secondary">Batal</a>
                </div>
            </div>
        </div>
    </div>
</form>
@endsection
